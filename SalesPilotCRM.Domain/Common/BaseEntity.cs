namespace SalesPilotCRM.Domain.Common
{
    public abstract class BaseEntity : IAuditable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
