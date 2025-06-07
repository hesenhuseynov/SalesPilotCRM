using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Interfaces.Auth;

namespace SalesPilotCRM.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
    {
        public readonly IAuthService _authService;


        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;


        }

        public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokenAsync(request.RefreshTokenRequest, cancellationToken);

        }
    }
}
