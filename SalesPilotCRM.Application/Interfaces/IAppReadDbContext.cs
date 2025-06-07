using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Interfaces
{
    public interface IAppReadDbContext
    {
        IQueryable<User> Users { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<Customer> Customers { get; }
        IQueryable<CustomerStatus> CustomerStatuses { get; }

        IQueryable<Deal> Deals { get; }
        IQueryable<DealStage> DealStages { get; }
        IQueryable<Activity> Activities { get; }
        IQueryable<ActivityType> ActivityTypes { get; }
        IQueryable<Notification> Notifications { get; }


    }
}
