using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(int Id) : IRequest<Result<GetCustomerByIdResponse>>;


    public class GetCustomerByIdResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CompanyName { get; set; }
        public string? Status { get; set; }
        public byte[] RowVersion { get; set; } = default!;


    }
}
