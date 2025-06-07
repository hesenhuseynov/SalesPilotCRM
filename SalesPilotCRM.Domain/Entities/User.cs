using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public bool IsLocked { get; set; } = false;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }

        public ICollection<Customer>? AssignedCustomers { get; set; }
        public ICollection<Deal>? AssignedDeals { get; set; }

        public ICollection<Activity> CreatedActivities { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
