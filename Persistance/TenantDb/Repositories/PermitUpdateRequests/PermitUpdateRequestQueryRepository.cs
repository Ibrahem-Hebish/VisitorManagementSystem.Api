
using Domain.TenantDomain.PermitUpdateRequests;
using Domain.TenantDomain.PermitUpdateRequests.ObjectValues;
using Domain.TenantDomain.PermitUpdateRequests.Repositories;
using Domain.TenantDomain.Users.ObjectValues;

namespace Persistence.TenantDb.Repositories.PermitUpdateRequests;

public class PermitUpdateRequestQueryRepository(TenantDbContext dbContext) : IPermitUpdateRequestQueryRepository
{
    public async Task<List<PermitUpdateRequest>> GetAllAsync()
    {
        return await dbContext.PermitUpdateRequests.ToListAsync();
    }

    public async Task<PermitUpdateRequest?> GetByIdAsync(PermitUpdateRequestId id)
    {
        return await dbContext.PermitUpdateRequests.FindAsync(id);
    }

    public async Task<List<PermitUpdateRequest>> GetByRequesterId(UserId userId)
    {
        return await dbContext.PermitUpdateRequests.Where(p => p.RequesterId == userId).ToListAsync();
    }

    public async Task<List<PermitUpdateRequest>> GetLatestAsync()
    {
        return await dbContext.PermitUpdateRequests.OrderByDescending(p => p.CreatedAt).Take(10).ToListAsync();
    }
}
