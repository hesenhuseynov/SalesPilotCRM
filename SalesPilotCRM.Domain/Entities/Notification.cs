using SalesPilotCRM.Domain.Common;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class Notification : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public int RecipientUserId { get; set; }
        public User RecipientUser { get; set; } = null!;

        public bool IsRead { get; set; } = false;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; set; }

        public string? RelatedEntityType { get; set; } // Optional: "Deal", "Activity" və s.

        public int? RelatedEntityId { get; set; }

        public required byte[] RowVersion { get; set; }

    }
}
