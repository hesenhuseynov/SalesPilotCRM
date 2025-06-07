using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SalesPilotCRM.Persistence.Contexts;

namespace SalesPilotCRM.Persistence.Factory
{
    public class AppWriteDbContextFactory : IDesignTimeDbContextFactory<AppWriteDbContext>
    {


        public AppWriteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppWriteDbContext>();
            var connectionString = "Server=ACER\\SQLEXPRESS01;Database=SalesPilotCRM;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
            return new AppWriteDbContext(optionsBuilder.Options);
        }


    }
}
