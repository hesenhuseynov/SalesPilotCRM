using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class DealStageConfiguration : IEntityTypeConfiguration<DealStage>
    {
        public void Configure(EntityTypeBuilder<DealStage> builder)
        {
            builder.ToTable("DealStages");

            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(ds => ds.Deals)
                   .WithOne(d => d.DealStage)
                   .HasForeignKey(d => d.DealStageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
