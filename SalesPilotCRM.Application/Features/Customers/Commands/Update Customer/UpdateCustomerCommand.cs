using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Customers.Commands.Update_Customer
{
    //public class UpdateCustomerCommand : IRequest<Result<UpdateCustomerResponse>>
    //{

    //    public UpdateCustomerDto UpdateCustomerDto { get; set; }


    //}

    public record UpdateCustomerCommand(UpdateCustomerDto UpdateCustomerDto) : IRequest<Result<UpdateCustomerResponse>>;


    public class UpdateCustomerResponse
    {
        public int CustomerId { get; set; }

    }
}
