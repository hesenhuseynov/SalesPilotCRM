using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Result<CreateRoleResponse>>
    {
        public CreateRoleDto RoleDto { get; set; } = null!;
    }

    public class CreateRoleResponse
    {
        public int RoleId { get; set; }
    }
}
