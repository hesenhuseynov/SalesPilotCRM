using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Interfaces
{
    public interface IAppWriteDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerStatus> CustomerStatuses { get; set; }

        DbSet<Deal> Deals { get; set; }
        DbSet<DealStage> DealStages { get; set; }
        DbSet<Activity> Activities { get; set; }
        DbSet<ActivityType> ActivityTypes { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Entry(object entity);

        void SetOriginalRowVersion<T>(T entity, byte[] rowVersion) where T : class;

    }
}
