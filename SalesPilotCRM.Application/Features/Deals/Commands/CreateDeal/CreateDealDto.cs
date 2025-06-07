namespace SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal
{
    public class CreateDealDto
    {
        public string DealName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime ExpectedCloseDate { get; set; }
        public string? Notes { get; set; }

        public int CustomerId { get; set; }
        public int AssignedToUserId { get; set; }
        public int DealStageId { get; set; }
    }
}
