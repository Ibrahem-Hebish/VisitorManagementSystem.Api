using Domain.TenantDomain.Belongings;
using Domain.TenantDomain.Belongings.ObjectValues;

namespace Domain.TenantDomain.Belongings.Repositories;

public interface IBelongingCommandRepository
{
    Task AddAsync(Belonging belonging);
    Task UpdateAsync(Belonging belonging);
    Task DeleteAsync(BelongingId belongingId);
}
