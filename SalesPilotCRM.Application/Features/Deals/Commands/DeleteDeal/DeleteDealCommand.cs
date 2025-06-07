using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Deals.Commands.DeleteDeal
{
    public class DeleteDealCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
