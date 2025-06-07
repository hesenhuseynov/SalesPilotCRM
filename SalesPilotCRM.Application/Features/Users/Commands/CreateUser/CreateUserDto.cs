namespace SalesPilotCRM.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
