using Domain.TenantDomain.Attachments;
using Domain.TenantDomain.Belongings;
using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.PermitUpdateRequests;
using Domain.TenantDomain.Roles;
using Domain.TenantDomain.Tenants;
using Domain.TenantDomain.Tokens;
using Domain.TenantDomain.Visitors;
using MediatR;
using Persistence.TenantDb.Configurations;
using UserConfiguration = Persistence.TenantDb.Configurations.UserConfiguration;

namespace Persistence.TenantDb;

public class TenantDbContext : DbContext
{
    private readonly ITenantService _tenantService;
    private readonly IPublisher _publisher;

    private string _branchId;

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Requester> Requesters { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Security> Securities { get; set; }
    public DbSet<Permit> Permits { get; set; }
    public DbSet<PermitTrack> PermitTracks { get; set; }
    public DbSet<PermitUpdateRequest> PermitUpdateRequests { get; set; }
    public DbSet<Entrylog> Entrylogs { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Belonging> Belongings { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<VisitorPermit> VisitorPermits { get; set; }



    public TenantDbContext(DbContextOptions<TenantDbContext> options,
    ITenantService tenantService,
    IPublisher publisher
    )
        : base(options)
    {
        _tenantService = tenantService;
        _publisher = publisher;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _tenantService.GetConnectionString();

        optionsBuilder.UseSqlServer(connectionString);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new BranchConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new RequesterConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        modelBuilder.ApplyConfiguration(new SecurityConfiguration());
        modelBuilder.ApplyConfiguration(new PermitConfiguration());
        modelBuilder.ApplyConfiguration(new PermitTrackConfiguration());
        modelBuilder.ApplyConfiguration(new PermitUpdateRequestConfiguration());
        modelBuilder.ApplyConfiguration(new EntryLogConfiguration());
        modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new BelongingConfiguration());
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new VisitorConfiguration());
        modelBuilder.ApplyConfiguration(new VisitorPermitConfiguration());


        //////////////
        _branchId = _tenantService.GetBranchId();

        modelBuilder.Entity<Permit>()
            .HasQueryFilter(p => p.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<PermitTrack>()
            .HasQueryFilter(pt => pt.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<PermitUpdateRequest>()
            .HasQueryFilter(pur => pur.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<Entrylog>()
            .HasQueryFilter(el => el.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<Attachment>()
            .HasQueryFilter(a => a.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<Belonging>()
            .HasQueryFilter(b => b.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<Building>()
            .HasQueryFilter(b => b.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<Visitor>()
            .HasQueryFilter(v => v.BranchId.Value.ToString() == _branchId);

        modelBuilder.Entity<VisitorPermit>()
            .HasQueryFilter(v => v.BranchId.Value.ToString() == _branchId);

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var branchId = _tenantService.GetBranchId();

        foreach (var entry in ChangeTracker.Entries<IMultiTenant>())
        {
            if (entry.State == EntityState.Added && entry.Entity.BranchId is null)
            {
                entry.Entity.BranchId = new BranchId(new Guid(branchId));
            }
        }

        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .SelectMany(x => x.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }


        return result;
    }
}

