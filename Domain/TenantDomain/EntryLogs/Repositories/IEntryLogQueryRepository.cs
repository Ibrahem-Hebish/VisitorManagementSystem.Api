using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.EntryLogs.ObjectValues;

namespace Domain.TenantDomain.EntryLogs.Repositories;

public interface IEntryLogQueryRepository
{
    Task<Entrylog?> GetByIdAsync(EntrylogId entrylogId, CancellationToken cancellationToken = default);
    Task<List<Entrylog>> GetAllAsync(CancellationToken cancellationToken = default);
}
