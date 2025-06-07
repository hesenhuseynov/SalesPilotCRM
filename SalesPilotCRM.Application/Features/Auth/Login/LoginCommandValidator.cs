using FluentValidation;

namespace SalesPilotCRM.Application.Features.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {

            RuleFor(x => x.LoginRequest.Email)
                   .NotEmpty().WithMessage("Email is required")
                   .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.LoginRequest.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
