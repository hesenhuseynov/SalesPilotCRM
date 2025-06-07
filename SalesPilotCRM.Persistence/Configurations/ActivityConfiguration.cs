using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title).HasMaxLength(150).IsRequired();

            builder.HasOne(a => a.Customer)
                   .WithMany(c => c.Activities)
                   .HasForeignKey(a => a.CustomerId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ActivityType)
                   .WithMany(at => at.Activities)
                   .HasForeignKey(a => a.ActivityTypeId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(a => a.CreatedByUser)
                .WithMany(u => u.CreatedActivities)
                 .HasForeignKey(a => a.CreatedById);

        }
    }

}
