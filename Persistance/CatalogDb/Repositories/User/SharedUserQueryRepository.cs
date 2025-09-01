namespace Persistence.CentralTenantsDatabase.Repositories.User;

public class SharedUserQueryRepository(SharedDbContext dbContext) : ISharedUserQueryRepository
{
    public async Task<List<SharedUser>> GetAllAsync(CancellationToken cancellationToken = default)
                                                                      => await dbContext.SharedUsers
                                                                               .AsNoTracking()
                                                                                .ToListAsync(cancellationToken);


    public async Task<SharedUser?> GetByEmailAsync(string email)
    {

        var user = await dbContext.SharedUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user is not null)
            await dbContext.Entry(user).Reference(x => x.Branch).LoadAsync();

        return user;
    }
    public async Task<SharedUser?> GetByIdAsync(SharedUserId id, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.SharedUsers
                                    .FindAsync(id, cancellationToken);

        if (user is not null)
            await dbContext.Entry(user).Reference(x => x.Branch).LoadAsync(cancellationToken);

        return user;
    }
}
