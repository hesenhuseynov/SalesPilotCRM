using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenCommand : IRequest<Result<RefreshTokenResponse>>
    {
        public RefreshTokenRequest RefreshTokenRequest { get; set; } = null!;

    }
}
