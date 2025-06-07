using FluentValidation;

namespace SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal
{
    public class CreateDealCommandValidator : AbstractValidator<CreateDealCommand>
    {

        public CreateDealCommandValidator()
        {
            RuleFor(x => x.DealDto.DealName)
             .NotEmpty().WithMessage("Deal name is required.")
             .MaximumLength(100).WithMessage("Deal name must not exceed 100 characters.");



            RuleFor(x => x.DealDto.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.DealDto.ExpectedCloseDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Expected close date cannot be in the past.");

            RuleFor(x => x.DealDto.CustomerId)
                .GreaterThan(0).WithMessage("Customer must be selected.");

            RuleFor(x => x.DealDto.DealStageId)
                .GreaterThan(0).WithMessage("Deal stage must be selected.");

            RuleFor(x => x.DealDto.AssignedToUserId)
                .GreaterThan(0).WithMessage("Assigned user must be specified.");
        }

    }
}
