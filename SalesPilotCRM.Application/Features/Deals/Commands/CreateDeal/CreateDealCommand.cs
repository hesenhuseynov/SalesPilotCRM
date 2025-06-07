using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal
{
    //public class CreateDealCommand : IRequest<Result<CreateDealResponse>>
    //{
    //    public CreateDealDto DealDto { get; set; }
    //}


    public record CreateDealCommand(CreateDealDto DealDto) : IRequest<Result<CreateDealResponse>>;

    public class CreateDealResponse
    {
        public int DealId { get; set; }
    }
}
