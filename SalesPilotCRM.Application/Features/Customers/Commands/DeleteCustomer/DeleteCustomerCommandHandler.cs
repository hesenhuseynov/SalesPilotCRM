using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<DeleteCustomerResponse>>
    {
        private readonly ICustomerService _customerService;


        public DeleteCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<Result<DeleteCustomerResponse>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.DeleteCustomerAsync(request.dto, cancellationToken);

            if (!result.Success)
            {
                return Result<DeleteCustomerResponse>.Fail(result.Errors!, result.Status);
            }

            return Result<DeleteCustomerResponse>.Ok(
                new DeleteCustomerResponse { CustomerId = result.Data },
                result.Message
                );
        }
    }
}
