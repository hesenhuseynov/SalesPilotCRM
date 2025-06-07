using Microsoft.EntityFrameworkCore;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Domain.Common;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Contexts
{
    public class AppWriteDbContext : DbContext, IAppWriteDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public AppWriteDbContext(DbContextOptions<AppWriteDbContext> options, ICurrentUserService currentUserService)
        : base(options) { _currentUserService = currentUserService; }



        // Used  EF Core design time support  migrations  when DI is not available
        public AppWriteDbContext(DbContextOptions<AppWriteDbContext> options)
            : this(options, new SystemCurrentUserService())
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealStage> DealStages { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }



        //public DatabaseFacade Database => base.Database;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppWriteDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                var userId = _currentUserService.UserId ?? "System";

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;

                    if (!int.TryParse(userId, out int parsedUserId))
                    {
                        parsedUserId = 1;
                        //Logger.LogWarning("Geçersiz UserId: {UserId}", userId);
                    }
                    entry.Entity.CreatedById = parsedUserId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public void SetOriginalRowVersion<T>(T entity, byte[] rowVersion) where T : class
        {
            Entry(entity).OriginalValues["RowVersion"] = rowVersion;
        }
    }
}
