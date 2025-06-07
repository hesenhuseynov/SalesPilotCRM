using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Deals.Commands.UpdateDeal
{
    public class UpdateDealCommandHandler : IRequestHandler<UpdateDealCommand, Result>
    {
        private readonly IDealService _dealService;

        public UpdateDealCommandHandler(IDealService dealService)
        {
            _dealService = dealService;
        }

        public async Task<Result> Handle(UpdateDealCommand request, CancellationToken cancellationToken)
        {
            var result = await _dealService.UpdateDealAsync(request.DealDto, cancellationToken);

            if (!result.Success)
                return Result.Fail(result.Errors!, result.Status);

            return Result.Ok(result.Message);
        }
    }
}
