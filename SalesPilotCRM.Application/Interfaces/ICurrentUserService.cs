namespace SalesPilotCRM.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }

        // List<string> Roles { get; }
        //bool IsAdmin { get; }
        // string? IpAddress { get; }
    }
}
