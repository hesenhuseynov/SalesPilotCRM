using Microsoft.Extensions.DependencyInjection;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Interfaces.Auth;
using SalesPilotCRM.Application.Services;
using SalesPilotCRM.Infrastructure.Hashing;
using SalesPilotCRM.Infrastructure.Logging;
using SalesPilotCRM.Infrastructure.Security;
using SalesPilotCRM.Infrastructure.Services;
using SalesPilotCRM.Infrastructure.Services.Auth;
using SalesPilotCRM.Infrastructure.Services.Customers;
using SalesPilotCRM.Infrastructure.Services.CustomerStatus;
using SalesPilotCRM.Infrastructure.Services.Deals;
using SalesPilotCRM.Infrastructure.Services.Users;

namespace SalesPilotCRM.Infrastructure
{
    public static class InfraServiceRegistiration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IDealService, DealService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerStatusService, CustomerStatusService>();
            services.AddScoped<IActivityService, ActivityService>();

            services.AddScoped<IUserService, UserService>();


            SerilogConfigurator.Configure();

            return services;
        }
    }
}
