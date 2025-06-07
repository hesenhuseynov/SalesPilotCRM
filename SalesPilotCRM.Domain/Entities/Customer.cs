using SalesPilotCRM.Domain.Common;
using SalesPilotCRM.Domain.Events;
using Serilog;
using Serilog.Context;

namespace SalesPilotCRM.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? CompanyName { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;


        public int CustomerStatusId { get; set; }
        public CustomerStatus CustomerStatus { get; set; } = null!;

        public string? Notes { get; set; }

        public int? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public DateTime? LastContactedDate { get; set; }

        public DateTime? NextFollowUpDate { get; set; }

        public int LeadScore { get; set; } = 0;

        public ICollection<Deal>? Deals { get; set; }
        public ICollection<Activity>? Activities { get; set; }


        public required byte[] RowVersion { get; set; }


        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();




        public void MarkAsCreated()
        {
            if (CreatedById.HasValue)
            {

                _domainEvents.Add(new CustomerCreatedEvent(
                    Id,
                    CreatedById.Value,
                    DateTime.UtcNow
                ));

            }
            else
            {
                // Loglama (Önemli!)
                using (LogContext.PushProperty("CustomerId", Id))
                {
                    Log.Logger.Error("CreatedById is null when calling MarkAsCreated. CustomerId: {CustomerId}", Id);
                }
                // İst                // İsteğe bağlı olarak istisna fırlatabilirsiniz
                // throw new InvalidOperationException("CreatedById is null.");
            }
        }

        public void ClearDomainEvents() => _domainEvents.Clear();




        //  elave ede bileceyim  modullar üçün navigation-lar xatirlatma sonradan 
        // public ICollection<CustomerTag> Tags { get; set; }
        // public ICollection<CustomerFile> Files { get; set; } 
    }
}
