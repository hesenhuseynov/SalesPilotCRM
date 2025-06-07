using FluentValidation;

namespace SalesPilotCRM.Application.Features.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.RegisterRequest.FullName)
                .NotEmpty().WithMessage("Full name is required");

            RuleFor(x => x.RegisterRequest.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.RegisterRequest.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
