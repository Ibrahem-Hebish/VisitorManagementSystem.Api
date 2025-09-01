namespace Persistence.CentralTenantsDatabase.Repositories.User;

public class SharedUserCommandRepository(SharedDbContext sharedDb) : ISharedUserCommandRepository
{
    public async Task AddAsync(SharedUser admin, CancellationToken cancellationToken = default)
    {
        sharedDb.SharedUsers.Add(admin);
        await sharedDb.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SharedUser admin, CancellationToken cancellationToken = default)
    {
        sharedDb.SharedUsers.Remove(admin);
        await sharedDb.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(SharedUser admin, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
