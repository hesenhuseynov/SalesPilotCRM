using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class CustomerStatusConfiguration : IEntityTypeConfiguration<CustomerStatus>
    {
        public void Configure(EntityTypeBuilder<CustomerStatus> builder)
        {
            builder.ToTable("CustomerStatuses");

            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.Name).HasMaxLength(100).IsRequired();
            builder.Property(cs => cs.Description).HasMaxLength(250);

            builder.HasMany(cs => cs.Customers)
                   .WithOne(c => c.CustomerStatus)
                   .HasForeignKey(c => c.CustomerStatusId)
                   .OnDelete(DeleteBehavior.Restrict);




        }
    }
}
