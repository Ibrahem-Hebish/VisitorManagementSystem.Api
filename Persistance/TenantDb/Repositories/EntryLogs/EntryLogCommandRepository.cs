using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.EntryLogs.Repositories;

namespace Persistence.TenantDb.Repositories.EntryLogs;

public class EntryLogCommandRepository(TenantDbContext dbContext) : IEntryLogCommandRepository
{
    public async Task AddAsync(Entrylog entrylog) => await dbContext.Entrylogs.AddAsync(entrylog);


    public void Delete(Entrylog entrylog) => dbContext.Entrylogs.Remove(entrylog);

    public void Update(Entrylog entrylog) => dbContext.Entrylogs.Update(entrylog);

}
