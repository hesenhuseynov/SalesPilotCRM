using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder.ToTable("Deals");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.DealName).HasMaxLength(150).IsRequired();
            builder.Property(d => d.Amount).HasColumnType("decimal(18,2)");

            builder.HasOne(d => d.Customer)
                   .WithMany(c => c.Deals)
                   .HasForeignKey(d => d.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.DealStage)
                   .WithMany(ds => ds.Deals)
                   .HasForeignKey(d => d.DealStageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.AssignedToUser)
                   .WithMany(u => u.AssignedDeals)
                   .HasForeignKey(d => d.AssignedToUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
