using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Interfaces;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace SalesPilotCRM.Application.Behaviors
{

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? "System";

            using (LogContext.PushProperty("RequestName", requestName))
            using (LogContext.PushProperty("UserId", userId))
            {
                var stopwatch = Stopwatch.StartNew();

                Log.Information("➡️ Handling {RequestName} by {UserId}", requestName, userId);

                try
                {
                    var response = await next();

                    stopwatch.Stop();

                    var success = response is Result r ? r.Success : true;
                    Log.Information("✅ {RequestName} handled in {ElapsedMilliseconds}ms - Success: {Success}", requestName, stopwatch.ElapsedMilliseconds, success);

                    return response;
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    Log.Error(ex, "❌ Error occurred while handling {RequestName}. Duration: {ElapsedMilliseconds}ms", requestName, stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }
    }
}
