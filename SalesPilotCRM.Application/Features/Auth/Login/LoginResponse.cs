namespace SalesPilotCRM.Application.Features.Auth.Login
{
    public class LoginResponse
    {

        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
