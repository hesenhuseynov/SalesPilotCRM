using FluentValidation;

namespace SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerDto.FirstName)
                      .NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.CustomerDto.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.CustomerDto.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.CustomerDto.Email));

            RuleFor(x => x.CustomerDto.CustomerStatusId)
                .GreaterThan(0).WithMessage("Customer status must be selected");
        }
    }
}
