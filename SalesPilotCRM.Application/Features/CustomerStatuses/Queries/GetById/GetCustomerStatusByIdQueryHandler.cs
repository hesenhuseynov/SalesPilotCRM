using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.CustomerStatuses.Queries.GetById
{
    public class GetCustomerStatusByIdQueryHandler
      : IRequestHandler<GetCustomerStatusByIdQuery, Result<CustomerStatusDto>>
    {
        private readonly ICustomerStatusService _customerStatusService;
        public GetCustomerStatusByIdQueryHandler(ICustomerStatusService customerStatusService)
        {
            _customerStatusService = customerStatusService;
        }

        public async Task<Result<CustomerStatusDto>> Handle(GetCustomerStatusByIdQuery request, CancellationToken cancellationToken)
        {
            return await _customerStatusService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}