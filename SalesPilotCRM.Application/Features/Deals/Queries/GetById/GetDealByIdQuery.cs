using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Deals.Queries.Dtos;

namespace SalesPilotCRM.Application.Features.Deals.Queries.GetById
{
    public class GetDealByIdQuery : IRequest<Result<DealDto>>
    {
        public int Id { get; set; }
    }
}
