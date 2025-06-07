namespace SalesPilotCRM.Application.Features.Activities.Commands
{
    public class CreateActivityDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.UtcNow;

        public int CreateByUserId { get; set; }

        public int ActivityTypeId { get; set; }
        public int? CustomerId { get; set; }
    }
}
