using Microsoft.AspNetCore.Mvc;

namespace SalesPilotCRM.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                {
                    var traceId = context.TraceIdentifier;
                    _logger.LogError(ex, "❌ Unhandled exception | TraceId: {TraceId}", traceId);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "An unexpected error occurred.",
                        Detail = "An unexpected error occurred. Please contact support.",
                        Instance = context.Request.Path,
                        Extensions = { ["traceId"] = traceId }
                    };
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }
            }
        }
    }

}
