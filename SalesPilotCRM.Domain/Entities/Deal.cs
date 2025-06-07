using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class Deal : BaseEntity
    {
        public string DealName { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime ExpectedCloseDate { get; set; }

        public string? Notes { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; } = null!;


        public int DealStageId { get; set; }
        public DealStage DealStage { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

    }
}
