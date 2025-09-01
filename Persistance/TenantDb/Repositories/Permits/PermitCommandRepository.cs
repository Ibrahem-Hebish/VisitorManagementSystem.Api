using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.Repositories;

namespace Persistence.TenantDb.Repositories.Permits;

public class PermitCommandRepository(TenantDbContext dbContext) : IPermitCommandRepository
{
    public async Task CreateAsync(Permit permit, CancellationToken cancellationToken = default)
                                                    => await dbContext.Permits.AddAsync(permit, cancellationToken);


    public void Delete(Permit permit) => dbContext.Permits.Remove(permit);

    public async Task DeleteDependenciesAsync(string permitId)
    {
        var id = new SqlParameter("@id", permitId);

        var sql = @"
        DELETE FROM Attachment WHERE PermitId = @id;
        DELETE FROM Belonging WHERE PermitId = @id;
        DELETE FROM PermitTrack WHERE PermitId = @id;
        DELETE FROM PermitUpdateRequest WHERE PermitId = @id;
        DELETE FROM EntryLog WHERE PermitId = @id;
    ";

        await dbContext.Database.ExecuteSqlRawAsync(sql, id);
    }

    public void Update(Permit permit) => dbContext.Permits.Update(permit);

}
