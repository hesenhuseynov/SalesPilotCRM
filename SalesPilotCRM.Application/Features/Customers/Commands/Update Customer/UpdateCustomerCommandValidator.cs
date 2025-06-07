using FluentValidation;

namespace SalesPilotCRM.Application.Features.Customers.Commands.Update_Customer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.UpdateCustomerDto.Id)
                .GreaterThan(0).WithMessage("Customer ID must be greater than 0");

            RuleFor(x => x.UpdateCustomerDto.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.UpdateCustomerDto.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.UpdateCustomerDto.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.UpdateCustomerDto.Email))
                .WithMessage("Invalid email format");

            RuleFor(x => x.UpdateCustomerDto.CustomerStatusId)
                .GreaterThan(0).WithMessage("Customer status must be selected");

            RuleFor(x => x.UpdateCustomerDto.RowVersion)
                .NotNull().WithMessage("RowVersion is required for concurrency check");
        }
    }
}
