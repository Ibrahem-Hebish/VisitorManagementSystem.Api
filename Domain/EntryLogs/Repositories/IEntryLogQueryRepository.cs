using Domain.EntryLogs.ObjectValues;

namespace Domain.EntryLogs.Repositories;

public interface IEntryLogQueryRepository
{
    Task<Entrylog?> GetByIdAsync(EntrylogId entrylogId, CancellationToken cancellationToken = default);
    Task<List<Entrylog>> GetAllAsync(CancellationToken cancellationToken = default);
}
