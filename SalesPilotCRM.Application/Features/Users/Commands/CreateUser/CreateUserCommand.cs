using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result<CreateUserResponse>>
    {
        public CreateUserDto UserDto { get; set; } = null!;
    }


    public class CreateUserResponse
    {
        public int UserId { get; set; }
    }
}
