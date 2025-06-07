using Microsoft.EntityFrameworkCore;
using SalesPilotCRM.Application.Common.Constants;
using SalesPilotCRM.Domain.Entities;
using SalesPilotCRM.Persistence.Contexts;

namespace SalesPilotCRM.Persistence.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(AppWriteDbContext context)
    {
        await context.Database.MigrateAsync();

        SeedRoles(context);
        SeedCustomerStatuses(context);
        SeedDealStages(context);
        SeedActivityTypes(context);

        await context.SaveChangesAsync();

        await SeedAdminUser(context);
        await SeedSalesAgent(context);
        SeedSampleCustomer(context);

        await context.SaveChangesAsync();
    }

    private static void SeedRoles(AppWriteDbContext context)
    {
        var existingRoleNames = context.Roles.Select(r => r.Name).ToList();
        var roleToSeed = new List<Role>
        {
            new Role { Name = RoleConstants.SystemAdmin, Description = "Full access to system", IsActive = true },
            new Role { Name = RoleConstants.SalesManager, Description = "Manage sales team and reports", IsActive = true },
            new Role { Name = RoleConstants.SalesAgent, Description = "Sales operations", IsActive = true },
            new Role { Name = RoleConstants.SupportManager, Description = "Manage support team", IsActive = true },
            new Role { Name = RoleConstants.SupportAgent, Description = "Handle tickets", IsActive = true },
        };



        var newRoles = roleToSeed.Where(r => !existingRoleNames.Contains(r.Name)).ToList();
        if (newRoles.Any())
        {
            context.Roles.AddRange(newRoles);
        }
    }

    private static void SeedCustomerStatuses(AppWriteDbContext context)
    {
        if (!context.CustomerStatuses.Any())
        {
            context.CustomerStatuses.AddRange(
                new CustomerStatus { Name = "New", Description = "New customer", IsActive = true },
                new CustomerStatus { Name = "Contacted", Description = "Contacted but not closed", IsActive = true },
                new CustomerStatus { Name = "Closed", Description = "Deal closed", IsActive = true }
            );
        }
    }

    private static void SeedDealStages(AppWriteDbContext context)
    {
        if (!context.DealStages.Any())
        {
            context.DealStages.AddRange(
                new DealStage { Name = "Lead", Order = 1 },
                new DealStage { Name = "Negotiation", Order = 2 },
                new DealStage { Name = "Closed Won", Order = 3 },
                new DealStage { Name = "Closed Lost", Order = 4 }
            );
        }
    }

    private static void SeedActivityTypes(AppWriteDbContext context)
    {
        if (context.ActivityTypes.Count() < 3)
        {
            context.ActivityTypes.AddRange(
                new ActivityType { Name = "Call", Icon = "📞", IsActive = true },
                new ActivityType { Name = "Email", Icon = "📧", IsActive = true },
                new ActivityType { Name = "Meeting", Icon = "📅", IsActive = true },
                new ActivityType { Name = "System Event", Icon = "⚙️", IsActive = true }
            );

        }
    }



    private static async Task SeedAdminUser(AppWriteDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleConstants.SystemAdmin);
            if (adminRole is null) return;

            CreatePasswordHash("Admin123!", out var passwordHash, out var passwordSalt);

            var admin = new User
            {
                FullName = "System Administrator",
                Email = "admin@crm.local",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = adminRole.Id,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "SeedScript"
            };

            context.Users.Add(admin);
        }
    }

    // ✅ NEW: Sales Agent seed
    private static async Task SeedSalesAgent(AppWriteDbContext context)
    {
        var salesAgentRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleConstants.SalesAgent);
        if (salesAgentRole == null) return;

        var exists = await context.Users.AnyAsync(u => u.RoleId == salesAgentRole.Id);
        if (exists) return;

        CreatePasswordHash("Agent123!", out var hash, out var salt);

        var agent = new User
        {
            FullName = "Sales Agent 1",
            Email = "agent@crm.local",
            PasswordHash = hash,
            PasswordSalt = salt,
            RoleId = salesAgentRole.Id,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "SeedScript"
        };

        context.Users.Add(agent);
    }

    // ✅ NEW: Sample customer
    private static void SeedSampleCustomer(AppWriteDbContext context)
    {
        if (!context.Customers.Any())
        {
            var status = context.CustomerStatuses.FirstOrDefault();
            var agent = context.Users.FirstOrDefault(u => u.Role.Name == RoleConstants.SalesAgent);

            if (status != null && agent != null)
            {
                context.Customers.Add(new Customer
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "+1234567890",
                    CompanyName = "TestCorp",
                    Address = "123 CRM Street",
                    CustomerStatusId = status.Id,
                    AssignedToUserId = agent.Id,
                    Notes = "Test customer seeded",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SeedScript"
                });
            }
        }
    }

    private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}
