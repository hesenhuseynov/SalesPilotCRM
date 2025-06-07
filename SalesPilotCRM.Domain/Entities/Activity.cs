using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class Activity : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime ActivityDate { get; set; }
        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; } = null!;

        public User CreatedByUser { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; } = null!;

        //public int CreatedByUserId { get; set; }
        //public User CreatedByUser { get; set; } = null!;
    }
}
