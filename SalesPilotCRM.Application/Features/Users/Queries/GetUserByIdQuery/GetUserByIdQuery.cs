using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers;

namespace SalesPilotCRM.Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public int Id { get; set; }

    }
}
