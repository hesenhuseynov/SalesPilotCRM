using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CreateCustomerResponse>>
    {

        public readonly ICustomerService _customerService;

        public CreateCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.CreateCustomerAsync(request.CustomerDto, cancellationToken);

            if (!result.Success)
            {
                return Result<CreateCustomerResponse>.Fail(result.Errors!, result.Status);

            }

            return Result<CreateCustomerResponse>.Ok(
                new CreateCustomerResponse { CustomerId = result.Data },
                result.Message);

        }
    }
}
