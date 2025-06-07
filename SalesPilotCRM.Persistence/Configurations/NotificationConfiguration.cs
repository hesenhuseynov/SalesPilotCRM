using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title).HasMaxLength(200).IsRequired();
            builder.Property(n => n.Message).HasMaxLength(1000).IsRequired();

            builder.HasOne(n => n.RecipientUser)
                   .WithMany()
                   .HasForeignKey(n => n.RecipientUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.RowVersion)
               .IsRowVersion()
              .IsConcurrencyToken();



            builder.Property(n => n.RelatedEntityType).HasMaxLength(100);
        }
    }

}
