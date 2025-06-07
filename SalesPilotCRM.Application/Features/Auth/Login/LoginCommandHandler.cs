using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Interfaces.Auth;

namespace SalesPilotCRM.Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {

        public readonly IAuthService _authService;



        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;

        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.LoginRequest, cancellationToken);
        }
    }
}