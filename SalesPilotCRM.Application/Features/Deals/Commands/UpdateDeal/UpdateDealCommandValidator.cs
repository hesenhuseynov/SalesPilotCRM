using FluentValidation;

namespace SalesPilotCRM.Application.Features.Deals.Commands.UpdateDeal
{

    public class UpdateDealCommandValidator : AbstractValidator<UpdateDealCommand>
    {
        public UpdateDealCommandValidator()
        {
            RuleFor(x => x.DealDto.Id)
                .GreaterThan(0).WithMessage("Deal ID must be greater than 0");

            RuleFor(x => x.DealDto.RowVersion)
                .NotNull().NotEmpty().WithMessage("RowVersion is required for concurrency check");

            RuleFor(x => x.DealDto.DealName)
                .NotEmpty().WithMessage("Deal name is required")
                .MaximumLength(150).WithMessage("Deal name cannot exceed 150 characters");

            RuleFor(x => x.DealDto.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero");

            RuleFor(x => x.DealDto.ExpectedCloseDate)
                .NotEmpty().WithMessage("Expected close date must be specified");

            RuleFor(x => x.DealDto.CustomerId)
                .GreaterThan(0).WithMessage("Customer must be selected");

            RuleFor(x => x.DealDto.AssignedToUserId)
                .GreaterThan(0).WithMessage("Assigned user must be selected");

            RuleFor(x => x.DealDto.DealStageId)
                .GreaterThan(0).WithMessage("Deal stage must be selected");
        }
    }

}
