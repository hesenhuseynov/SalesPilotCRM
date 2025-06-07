using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(250);

            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role)
                   .HasForeignKey(u => u.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
