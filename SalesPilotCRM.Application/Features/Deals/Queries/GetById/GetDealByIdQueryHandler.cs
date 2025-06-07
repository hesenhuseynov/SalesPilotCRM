using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Deals.Queries.Dtos;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Deals.Queries.GetById
{
    public class GetDealByIdQueryHandler : IRequestHandler<GetDealByIdQuery, Result<DealDto>>
    {
        private readonly IDealService _dealService;

        public GetDealByIdQueryHandler(IDealService dealService)
        {
            _dealService = dealService;
        }

        public async Task<Result<DealDto>> Handle(GetDealByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _dealService.GetByIdAsync(request.Id, cancellationToken);

            if (!result.Success)
                return Result<DealDto>.Fail(result.Errors!, result.Status);

            return Result<DealDto>.Ok(result.Data!, result.Message);
        }
    }
}
