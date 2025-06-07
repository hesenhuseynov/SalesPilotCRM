using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.CustomerStatuses.Queries.GetById
{
    public record GetCustomerStatusByIdQuery(int Id)
      : IRequest<Result<CustomerStatusDto>>;
}
