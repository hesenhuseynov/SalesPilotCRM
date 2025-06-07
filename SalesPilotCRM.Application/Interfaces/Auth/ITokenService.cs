using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();

    }
}
