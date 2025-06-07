using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName).HasMaxLength(150).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(150).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordSalt).IsRequired();

            builder.Property(x => x.RowVersion)
       .IsRowVersion()
       .IsConcurrencyToken();
        }
    }
}
