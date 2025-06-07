using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Events
{
    public sealed record CustomerCreatedEvent(int CustomerId, int CreatedByUserId, DateTime OccuredAt) : IDomainEvent;

    public sealed record ActivityCreatedEvent(int ActivityId, int CreatedByUserId, DateTime OccureedAt) : IDomainEvent;

}
