
using Application.Services.UnitOfWork;

namespace Persistence.TenantDb.UnitOfWork;

public class UnitOfWork(TenantDbContext dbContext) : IUnitOfWork
{
    public void Dispose() => dbContext.Dispose();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
                                                   => dbContext.SaveChangesAsync(cancellationToken);
    public Task BeginTransactionAsync() => dbContext.Database.BeginTransactionAsync();
    public Task CommitTransactionAsync() => dbContext.Database.CommitTransactionAsync();
    public Task RollbackTransactionAsync() => dbContext.Database.RollbackTransactionAsync();

}

