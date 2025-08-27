using Domain.EntryLogs.ObjectValues;

namespace Domain.EntryLogs.Repositories;

public interface IEntryLogCommandRepository
{
    Task AddAsync(Entrylog entrylog);
    Task UpdateAsync(Entrylog entrylog);
    Task DeleteAsync(EntrylogId entrylogId);
}
