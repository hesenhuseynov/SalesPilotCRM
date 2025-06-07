using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(150);
            builder.Property(c => c.Phone).HasMaxLength(50);

            builder.HasOne(c => c.CustomerStatus)
                   .WithMany(cs => cs.Customers)
                   .HasForeignKey(c => c.CustomerStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.IsActive).HasDefaultValue(true);

            builder.HasOne(c => c.AssignedToUser)
                   .WithMany(u => u.AssignedCustomers)
                   .HasForeignKey(c => c.AssignedToUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDeleted && x.IsActive);

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

        }
    }
}
