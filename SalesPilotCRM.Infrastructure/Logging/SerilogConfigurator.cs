using Serilog;

namespace SalesPilotCRM.Infrastructure.Logging
{
    public static class SerilogConfigurator
    {
        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "SalesPilotCRM")
                  .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }
    }
}
