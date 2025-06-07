using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class CustomerStatus : BaseEntity
    {

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Customer>? Customers { get; set; }

    }

}
