using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Persistence.Contexts;

namespace SalesPilotCRM.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppWriteDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            services.AddDbContext<AppReadDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IAppWriteDbContext>(provider => provider.GetRequiredService<AppWriteDbContext>());
            services.AddScoped<IAppReadDbContext>(provider => provider.GetRequiredService<AppReadDbContext>());


            return services;
        }

    }
}
