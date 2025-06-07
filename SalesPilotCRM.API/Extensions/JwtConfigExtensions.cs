using SalesPilotCRM.API.Services;
using SalesPilotCRM.Application.Common.Settings;

namespace SalesPilotCRM.API.Extensions
{
    public static class JwtConfigExtensions
    {
        public static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration, GoogleSecretService secretService)
        {
            var settings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
            settings.Key = secretService.GetSecret("jwt-secret", "salespilotcrm");

            services.Configure<JwtSettings>(opt =>
            {
                opt.Issuer = settings.Issuer;
                opt.Audience = settings.Audience;
                opt.Key = settings.Key;
            });

            return services;
        }



    }
}
