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
                               Role.Create("Admin"),
                               Role.Create("BranchAdmin"),
                               Role.Create("Manager"),
                               Role.Create("Requester"),
                               Role.Create("Security")
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

            User admin = new("Ibrahem", "Hebish", "ibrahemhebish@gmail.com", hashedPassword, "1234567890");

            admin.SetRole(adminRole);

            await dbContext.Users.AddAsync(admin);

            await dbContext.SaveChangesAsync();
        }

    }
}
