namespace SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public int CustomerStatusId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string? Notes { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
    }
}
