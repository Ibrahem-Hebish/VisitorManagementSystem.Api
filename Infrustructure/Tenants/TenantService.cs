using Domain.TenantDomain.Roles;
using Domain.TenantDomain.Roles.Enums;
using Domain.TenantDomain.Tenants;

namespace Infrustructure.Tenants;

public class TenantService : ITenantService
{
    private string? _tenantId;
    private string _branchId;

    private string? _tenantName;

    private string _connectionString;
    public TenantService()
    {

    }
    public string GetConnectionString() => _connectionString;

    public string? GetTenantId() => _tenantId;

    public string? GetTenantName() => _tenantName;

    public string? GetBranchId() => _branchId;

    public void SetTenantId(string tenantId)
    {
        Ensure.NotNullOrEmpty(tenantId, nameof(tenantId));
        _tenantId = tenantId;
    }

    public void SetTenantName(string tenantName)
    {
        Ensure.NotNullOrEmpty(tenantName, nameof(tenantName));
        _tenantName = tenantName;
    }

    public void SetConnectionString(string connectionString)
    {
        Ensure.NotNullOrEmpty(connectionString, nameof(connectionString));
        _connectionString = connectionString;
    }

    public void SetBranchId(string branchId) => _branchId = branchId;

    public async Task<(string, string)> CreateDatabaseForTenant(Tenant tenant, IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<TenantDbContext>();

        dbContext.Database.SetConnectionString(_connectionString);

        try
        {
            await dbContext.Database.MigrateAsync();

            dbContext.Tenants.Add(tenant);

            dbContext.Roles.AddRange(
                               Role.Create(Roles.Admin.ToString()),
                               Role.Create(Roles.TenantAdmin.ToString()),
                               Role.Create(Roles.BranchAdmin.ToString()),
                               Role.Create(Roles.Manager.ToString()),
                               Role.Create(Roles.Requester.ToString()),
                               Role.Create(Roles.Security.ToString())
                                                         );

            await dbContext.SaveChangesAsync();

            return (tenant.Id.Value.ToString(), tenant.Name);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error migrating database for tenant.Name: {ex.Message}");

            return (null!, null!);
        }
    }

    public async Task DeleteDatabaseForTenant(IPublisher publisher)
    {

        var dbContextOptions = new DbContextOptionsBuilder<TenantDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        TenantDbContext context = new(dbContextOptions, this, publisher);

        await context.Database.EnsureDeletedAsync();
    }

    public async Task SetConnectionStringForSignIn(string email, IServiceProvider serviceProvider)
    {
        var sharedUserQueryRepository = serviceProvider.GetRequiredService<ISharedUserQueryRepository>();

        var sharedBranchQueryRepository = serviceProvider.GetRequiredService<ISharedBranchQueryRepository>();

        var sharedTenantQueryRepository = serviceProvider.GetRequiredService<ISharedTenantQueryRepository>();

        var connecionStringProtector = serviceProvider.GetRequiredService<IConnectionStringProtector>();

        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var sharedUser = await sharedUserQueryRepository.GetByEmailAsync(email);

        if (sharedUser is null)
            throw new Exception("Invalid email or password.");

        var defaultConnectionString = configuration.GetSection("TenantConnection").Value;

        if (defaultConnectionString is null)
        {
            Log.Error("Tennant connection string is missing.");

            throw new Exception("Error While processing your request");

        }

        if (sharedUser.BranchId is not null)
        {
            var branch = await sharedBranchQueryRepository.GetByIdAsync(sharedUser.BranchId);

            if (branch is not null)
            {
                var tenant = await sharedTenantQueryRepository.GetByIdAsync(branch.TenantId);

                _connectionString = connecionStringProtector.Decrypt(tenant!.ConnectionString);

                _tenantId = tenant.Id.Value.ToString();

                _tenantName = tenant.Name;

                _branchId = branch.Id.Value.ToString();
            }
        }
        else
            _connectionString = defaultConnectionString;

    }

    public async Task SetConnectionStringForResetPassword(string email, IServiceProvider serviceProvider)
    {
        await SetConnectionStringForSignIn(email, serviceProvider);
    }
    public async Task<SharedUserToken> SetConnectionStringRefreshToken(string userTokenId, IServiceProvider serviceProvider)
    {
        var sharedUserTokenQueryRepository = serviceProvider.GetRequiredService<ISharedUserTokenQueryRepository>();

        var sharedBranchQueryRepository = serviceProvider.GetRequiredService<ISharedBranchQueryRepository>();

        var sharedTenantQueryRepository = serviceProvider.GetRequiredService<ISharedTenantQueryRepository>();

        var connecionStringProtector = serviceProvider.GetRequiredService<IConnectionStringProtector>();

        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var sharedToken = await sharedUserTokenQueryRepository.GetByIdAsync(
                                              new SharedUserTokenId(new Guid(userTokenId)));

        if (sharedToken is null)
            throw new Exception("User token not found");


        if (sharedToken.BranchId is not null)
        {
            var branch = await sharedBranchQueryRepository.GetByIdAsync(sharedToken.BranchId);

            if (branch is not null)
            {
                this._tenantName = (branch.Name);

                this._branchId = (branch.Id.Value.ToString());

                var tenant = await sharedTenantQueryRepository.GetByIdAsync(branch.TenantId);

                if (tenant is null)
                    throw new Exception("tenant is not found.");

                var tenantConnectionString = connecionStringProtector.Decrypt(tenant.ConnectionString);

                this._connectionString = (tenantConnectionString);
            }
        }
        else
        {
            var defaultConnectionString = configuration.GetSection("TenantConnection").Value;

            if (defaultConnectionString is null)
                throw new Exception("Error While proccessing your request");

            this._connectionString = (defaultConnectionString);
        }

        return sharedToken;

    }

    public async Task SetConnectionStringForChangePassword(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var role = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

        if (role is null)
            throw new UnauthorizedAccessException("User role not found in claims.");

        if (role == Roles.Admin.ToString())
        {
            var connectionString = configuration["SharedTenant"];

            ArgumentNullException.ThrowIfNull(connectionString);

            _connectionString = connectionString;
        }
        else
        {
            var sharedTenantQueryRepository = serviceProvider.GetRequiredService<ISharedTenantQueryRepository>();

            var connectionStringProtector = serviceProvider.GetRequiredService<IConnectionStringProtector>();

            var tenantId = httpContextAccessor.HttpContext?.User.FindFirstValue("TenantId");

            if (tenantId is null)
                throw new UnauthorizedAccessException("User tenant id is not found in claims.");

            var tenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(new Guid(tenantId)));

            if (tenant is null)
                throw new UnauthorizedAccessException("User tenant is not found.");

            var connectionString = tenant.ConnectionString;

            connectionString = connectionStringProtector.Decrypt(connectionString);

            _connectionString = connectionString;
        }

    }

}

