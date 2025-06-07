using Serilog;
using Serilog.Context;

namespace SalesPilotCRM.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        public readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var traceId = context.TraceIdentifier;
            var user = context.User.Identity?.Name;

            using (LogContext.PushProperty("TraceId", traceId))
            using (LogContext.PushProperty("User", user))
            {

                Log.Information("➡️ Incoming HTTP {Method} {Path}{Query} | TraceId: {TraceId}",
                   request.Method,
                   request.Path,
                   request.QueryString.HasValue ? request.QueryString.Value : "",
                   traceId);

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();


                await _next(context);

                stopwatch.Stop();

                var statusCode = context.Response?.StatusCode;


                Log.Information("✅ Handled {Method} {Path} with {StatusCode} in {ElapsedMilliseconds}ms | TraceId: {TraceId}",
                    request.Method,
                    request.Path,
                    statusCode,
                    stopwatch.ElapsedMilliseconds,
                    traceId);

            }
        }

    }
}
