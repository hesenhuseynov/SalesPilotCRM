using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;

namespace SalesPilotCRM.Application.Features.Customers.Queries.GetAll
{
    public record GetAllCustomersQuery() : IRequest<Result<List<CustomerDto>>>;

}
