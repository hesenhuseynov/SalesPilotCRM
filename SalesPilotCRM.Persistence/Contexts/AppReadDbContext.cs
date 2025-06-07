using Microsoft.EntityFrameworkCore;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Contexts
{
    public class AppReadDbContext : DbContext, IAppReadDbContext
    {

        public AppReadDbContext(DbContextOptions<AppReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppReadDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<User> Users => Set<User>().AsNoTracking();
        public IQueryable<Role> Roles => Set<Role>().AsNoTracking();
        public IQueryable<Customer> Customers => Set<Customer>().AsNoTracking();
        public IQueryable<CustomerStatus> CustomerStatuses => Set<CustomerStatus>().AsNoTracking();
        public IQueryable<Deal> Deals => Set<Deal>().AsNoTracking();
        public IQueryable<DealStage> DealStages => Set<DealStage>().AsNoTracking();
        public IQueryable<Activity> Activities => Set<Activity>().AsNoTracking();
        public IQueryable<ActivityType> ActivityTypes => Set<ActivityType>().AsNoTracking();
        public IQueryable<Notification> Notifications => Set<Notification>().AsNoTracking();


    }
}
