using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.ToTable("ActivityTypes");

            builder.HasKey(at => at.Id);

            builder.Property(at => at.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(at => at.Activities)
                   .WithOne(a => a.ActivityType)
                   .HasForeignKey(a => a.ActivityTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
