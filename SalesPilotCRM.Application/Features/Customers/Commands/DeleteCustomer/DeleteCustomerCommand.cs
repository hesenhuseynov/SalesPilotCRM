using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Customers.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(DeleteCustomerDto dto) : IRequest<Result<DeleteCustomerResponse>>;



    public class DeleteCustomerResponse
    {
        public int CustomerId { get; set; }

    }
}
