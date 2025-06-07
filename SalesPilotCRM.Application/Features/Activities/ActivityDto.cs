namespace SalesPilotCRM.Application.Features.Activity
{
    public class ActivityDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityTypeName { get; set; } = null!;
        public string? CustomerFullName { get; set; }
        public string CreatedByUserName { get; set; } = null!;
    }
}
