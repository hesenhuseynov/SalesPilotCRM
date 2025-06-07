using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Customers.Commands.Update_Customer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<UpdateCustomerResponse>>
    {

        private readonly ICustomerService _customerService;

        public UpdateCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<Result<UpdateCustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.UpdateCustomerAsync(request.UpdateCustomerDto, cancellationToken);

            if (!result.Success)
            {
                return Result<UpdateCustomerResponse>.Fail(result.Errors!, result.Status);

            }

            return Result<UpdateCustomerResponse>.Ok(
                new UpdateCustomerResponse { CustomerId = result.Data }, result.Message
                );

        }
    }
}
