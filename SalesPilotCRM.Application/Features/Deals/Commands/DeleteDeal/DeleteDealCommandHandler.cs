using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Deals.Commands.DeleteDeal
{
    public class DeleteDealCommandHandler : IRequestHandler<DeleteDealCommand, Result>
    {
        private readonly IDealService _dealService;

        public DeleteDealCommandHandler(IDealService dealService)
        {
            _dealService = dealService;
        }

        public async Task<Result> Handle(DeleteDealCommand request, CancellationToken cancellationToken)
        {
            var result = await _dealService.DeleteDealAsync(request.Id, request.RowVersion, cancellationToken);

            if (!result.Success)
                return Result.Fail(result.Errors!, result.Status);

            return Result.Ok(result.Message);
        }
    }
}
