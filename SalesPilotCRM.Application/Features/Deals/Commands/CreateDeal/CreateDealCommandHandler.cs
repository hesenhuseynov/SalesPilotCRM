using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal
{
    public class CreateDealCommandHandler : IRequestHandler<CreateDealCommand, Result<CreateDealResponse>>
    {

        private readonly IDealService _dealService;

        public CreateDealCommandHandler(IDealService dealService)
        {
            _dealService = dealService;

        }



        public async Task<Result<CreateDealResponse>> Handle(CreateDealCommand request, CancellationToken cancellationToken)
        {
            var result = await _dealService.CreateDealAsync(request.DealDto, cancellationToken);

            if (!result.Success)
            {
                return Result<CreateDealResponse>.Fail(result.Errors!, result.Status);

            }

            var response = new CreateDealResponse { DealId = result.Data };
            return Result<CreateDealResponse>.Ok(response, result.Message);
        }
    }
}
