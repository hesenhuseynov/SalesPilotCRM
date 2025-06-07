using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class DealStage : BaseEntity
    {
        public string Name { get; set; } = null!;

        public int Order { get; set; }

        public bool IsFinal { get; set; } = false;

        public ICollection<Deal>? Deals { get; set; }



    }
}
