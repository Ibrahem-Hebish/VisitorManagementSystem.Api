using Domain.TenantDomain.EntryLogs;

namespace Domain.TenantDomain.EntryLogs.Repositories;

public interface IEntryLogCommandRepository
{
    Task AddAsync(Entrylog entrylog);
    void Update(Entrylog entrylog);
    void Delete(Entrylog entrylog);
}
