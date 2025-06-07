using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class ActivityType : BaseEntity
    {
        public string Name { get; set; } = null!; // "Call", "Email", ve sayre 

        public string? Icon { get; set; } // UI ucun : "phone", "envelope", "calendar"

        public bool IsActive { get; set; } = true;

        public ICollection<Activity>? Activities { get; set; }

    }
}
