using FluentValidation;

namespace SalesPilotCRM.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleDto.Name)
                .NotEmpty().WithMessage("Role name is required");
        }
    }
}
