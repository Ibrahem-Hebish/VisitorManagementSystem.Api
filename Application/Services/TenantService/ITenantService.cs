using Domain.Tenants;

namespace Application.Services.TenantService;

public interface ITenantService
{
    string GetConnectionString();
    string? GetTenantId();
    string? GetTenantName();
    string? GetBranchId();
    Task DeleteDatabaseForTenant(IPublisher publisher);
    Task<(string, string)> CreateDatabaseForTenant(Tenant tenant, IServiceProvider serviceProvider);
    void SetTenantId(string tenantId);
    void SetTenantName(string tenantName);
    void SetConnectionString(string connectionString);
    void SetBranchId(string branchId);
    Task SetConnectionStringForSignIn(string email, IServiceProvider serviceProvider);
    Task<SharedUserToken> SetConnectionStringRefreshToken(string userTokenId, IServiceProvider serviceProvider);
}
