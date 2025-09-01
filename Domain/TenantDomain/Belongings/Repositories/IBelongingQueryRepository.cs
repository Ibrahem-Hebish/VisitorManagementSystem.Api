using Domain.TenantDomain.Belongings;
using Domain.TenantDomain.Belongings.ObjectValues;

namespace Domain.TenantDomain.Belongings.Repositories;

public interface IBelongingQueryRepository
{
    Task<Belonging?> GetByIdAsync(BelongingId belongingId);
    Task<List<Belonging>> GetAllAsync();
}
