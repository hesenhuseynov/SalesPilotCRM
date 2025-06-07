using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;

namespace SalesPilotCRM.Application.Features.Customers.Queries.GetByStatus
{

    public record GetCustomerListByStatusQuery(int StatusId)
        : IRequest<Result<List<CustomerDto>>>;
}
