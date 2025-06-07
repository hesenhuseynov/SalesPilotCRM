using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Result<CreateCustomerResponse>>
    {
        public CreateCustomerDto CustomerDto { get; set; } = null!;

    }
    public class CreateCustomerResponse
    {
        public int CustomerId { get; set; }
    }
}
