using FluentValidation;

namespace SalesPilotCRM.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserDto.FullName)
                .NotEmpty().WithMessage("Full name is required");
            RuleFor(x => x.UserDto.Email)
           .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.UserDto.RoleId)
                .GreaterThan(0).WithMessage("RoleId must be provided");
        }
    }
}
