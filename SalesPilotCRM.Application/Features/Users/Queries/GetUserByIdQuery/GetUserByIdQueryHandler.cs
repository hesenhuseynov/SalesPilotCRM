using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
