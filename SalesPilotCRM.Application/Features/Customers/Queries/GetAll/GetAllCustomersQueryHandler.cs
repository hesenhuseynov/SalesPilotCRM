using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result<List<CustomerDto>>>
    {
        private readonly ICustomerService _customerService;


        public GetAllCustomersQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;

        }


        public async Task<Result<List<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {

            return await _customerService.GetAllCustomerList(cancellationToken);

        }
    }
}
