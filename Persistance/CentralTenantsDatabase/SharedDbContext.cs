using UserConfiguration = Persistence.CentralTenantsDatabase.Configurations.UserConfiguration;

namespace Persistence.CentralTenantsDatabase;

public class SharedDbContext : DbContext
{
    public DbSet<SharedTenant> SharedTenants { get; set; }
    public DbSet<SharedBranch> SharedBranches { get; set; }
    public DbSet<SharedUser> SharedUsers { get; set; }
    public DbSet<SharedUserToken> SharedUserTokens { get; set; }

    public SharedDbContext(DbContextOptions<SharedDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsetting.json")
            .Build();

        var connectionString = config.GetSection("DefaultConnection").Value;


        optionsBuilder.UseSqlServer("server = . ; database = Shared; Integrated Security = SSPI;TrustServerCertificate = true");


        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new SharedTenantConfiguration());
        modelBuilder.ApplyConfiguration(new SharedBranchConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SharedUserTokenConfiguration());
    }


}

