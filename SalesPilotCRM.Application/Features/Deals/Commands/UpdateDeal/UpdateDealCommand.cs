using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Deals.Commands.UpdateDeal
{
    public class UpdateDealCommand : IRequest<Result>
    {
        public UpdateDealDto DealDto { get; set; } = null!;
    }
}
