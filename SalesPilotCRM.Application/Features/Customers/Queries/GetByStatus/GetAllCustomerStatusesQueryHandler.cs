using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Customers.Queries.GetByStatus
{

    public class GetCustomerListByStatusQueryHandler
      : IRequestHandler<GetCustomerListByStatusQuery, Result<List<CustomerDto>>>
    {
        private readonly ICustomerService _customerService;
        public GetCustomerListByStatusQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<Result<List<CustomerDto>>> Handle(GetCustomerListByStatusQuery request, CancellationToken cancellationToken)
        {
            return await _customerService.GetCustomersByStatusAsync(request.StatusId, cancellationToken);
        }


    }

}
