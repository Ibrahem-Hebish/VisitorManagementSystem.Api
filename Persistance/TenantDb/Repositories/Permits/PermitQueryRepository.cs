using Domain.Permits.Repositories;

namespace Persistence.TenantDb.Repositories.Permits;

public class PermitQueryRepository(TenantDbContext dbContext) : IPermitQueryRepository
{
    public async Task<IReadOnlyList<Permit>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.AsNoTracking()
                                         .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Permit?> GetByIdAsync(PermitId id, CancellationToken cancellationToken = default)
    {
        var permit = await dbContext.Permits.FirstOrDefaultAsync(p => p.PermitId == id, cancellationToken: cancellationToken);

        return permit;
    }

    public async Task<Permit?> GetDetailsAsync(PermitId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits
                               .AsNoTracking()
                               .Include(p => p.Belongings)
                               .Include(p => p.Attachments)
                               .FirstOrDefaultAsync(p => p.PermitId == id, cancellationToken: cancellationToken);
    }

    public async Task<Entrylog?> GetEntryLogAsync(PermitId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Entrylogs
                               .AsNoTracking()
                                .FirstOrDefaultAsync(el => el.PermitId == id, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Permit>> GetLatestAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.OrderByDescending(p => p.StartDate)
                                       .AsNoTracking()
                                       .Take(count)
                                       .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Permit>> GetPagedAsync(int pageNumber, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.OrderByDescending(p => p.StartDate)
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Permit>> GetPermitsCreatedByRequester(UserId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.Where(p => p.RequestedBy == id)
                                       .AsNoTracking()
                                        .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Permit>> GetPermitsHandledByManager(UserId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.Where(p => p.HandledBy == id)
                                       .AsNoTracking()
                                        .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<PermitTrack>> GetTracksAsync(PermitId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.PermitTracks.Where(pt => pt.PermitId == id)
                                            .AsNoTracking()
                                             .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Permit>> GetVisitorPermitsAsync(VisitorId visitorId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.Where(p => p.VisitorId == visitorId)
                                       .AsNoTracking()
                                        .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Permit>> SearchByDateAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.Where(p => p.StartDate >= startDate && p.EndDate <= endDate)
                                       .AsNoTracking()
                                        .ToListAsync(cancellationToken: cancellationToken);
    }
}
