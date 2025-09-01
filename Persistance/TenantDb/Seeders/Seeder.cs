using Domain.TenantDomain.Roles;
using Domain.TenantDomain.Roles.Enums;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.Enums;

namespace Persistence.TenantDb.Seeders;

public class Seeder()
{

    public static async Task SeedRoles(TenantDbContext dbContext, ITenantService tenantService, IConfiguration configuration)
    {
        var sharedConnectionString = configuration.GetSection("TenantConnection").Value;

        if (string.IsNullOrEmpty(sharedConnectionString))
            throw new InvalidOperationException("Tenant connection string is not configured.");

        tenantService.SetConnectionString(sharedConnectionString);

        if (!dbContext.Roles.Any())
        {
            dbContext.Roles.AddRange(
                               Role.Create(Roles.Admin.ToString()),
                               Role.Create(Roles.TenantAdmin.ToString()),
                               Role.Create(Roles.BranchAdmin.ToString()),
                               Role.Create(Roles.Manager.ToString()),
                               Role.Create(Roles.Requester.ToString()),
                               Role.Create(Roles.Security.ToString())
                                                         );
            await dbContext.SaveChangesAsync();
        }
    }

    public static async Task SeedAdmin(TenantDbContext dbContext, IPasswordHashingService passwordHashingService,
        ITenantService tenantService, IConfiguration configuration)
    {
        var sharedConnectionString = configuration.GetSection("TenantConnection").Value;

        if (string.IsNullOrEmpty(sharedConnectionString))
            throw new InvalidOperationException("Tenant connection string is not configured.");

        tenantService.SetConnectionString(sharedConnectionString);

        if (!dbContext.Users.Any())
        {
            var adminRole = dbContext.Roles.FirstOrDefault(r => r.Name == "Admin");

            if (adminRole == null)
                throw new InvalidOperationException("Admin role not found.");

            var hashedPassword = passwordHashingService.HashPasswordBCrypt("Hema123#");

            User admin = new("Ibrahem", "Hebish", "ibrahemhebish@gmail.com", hashedPassword, "1234567890", PersonGender.Male);

            admin.SetRole(adminRole);

            await dbContext.Users.AddAsync(admin);

            await dbContext.SaveChangesAsync();
        }

    }
}
