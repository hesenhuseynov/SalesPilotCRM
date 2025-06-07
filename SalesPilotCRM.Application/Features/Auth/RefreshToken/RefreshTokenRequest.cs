namespace SalesPilotCRM.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = null!;
        public string Email { get; set; } = null!;
    }


}
