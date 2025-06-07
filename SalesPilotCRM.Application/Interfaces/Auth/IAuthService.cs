using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Auth.Login;
using SalesPilotCRM.Application.Features.Auth.RefreshToken;
using SalesPilotCRM.Application.Features.Auth.Register;

namespace SalesPilotCRM.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToke = default!);
    }
}
