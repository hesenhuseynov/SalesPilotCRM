namespace SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } = null!;
        public string? RoleName { get; set; } // Eger Role daxildir isə
    }
}
