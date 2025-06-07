namespace SalesPilotCRM.Application.Features.Deals.Commands.UpdateDeal
{
    public class UpdateDealDto
    {
        public int Id { get; set; }
        public string DealName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime ExpectedCloseDate { get; set; }
        public string? Notes { get; set; }
        public int CustomerId { get; set; }
        public int AssignedToUserId { get; set; }
        public int DealStageId { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

}
