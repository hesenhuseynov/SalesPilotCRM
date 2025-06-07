namespace SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? StatusName { get; set; }
    }
}
