using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<Result<List<UserDto>>>
    {

    }
}
