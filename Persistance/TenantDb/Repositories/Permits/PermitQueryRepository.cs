using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.Permits.Repositories;
using Domain.TenantDomain.PermitTracks;
using Domain.TenantDomain.Users.ObjectValues;

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
        var permit = await dbContext.Permits.FindAsync(id);

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

    public async Task<IReadOnlyList<Permit>> GetPagedAsync(PermitId? id, DateTime? cursorDate, PaginationDirection direction, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Permits.AsQueryable();

        if (direction == PaginationDirection.Forward)
        {
            query = query
                .OrderByDescending(p => p.StartDate).ThenByDescending(p => p.PermitId);

            if (cursorDate.HasValue && id is not null)
                query = query
                    .Where(p => p.StartDate < cursorDate ||
                               (p.StartDate == cursorDate && p.PermitId.Value.CompareTo(id.Value) < 0));
        }
        else
        {
            query = query
                .OrderBy(p => p.StartDate).ThenBy(p => p.PermitId);

            if (cursorDate.HasValue && id is not null)
                query = query
                    .Where(p => p.StartDate > cursorDate ||
                                (p.StartDate == cursorDate && p.PermitId.Value.CompareTo(id.Value) > 0));
        }

        var result = await query
            .Take(10)
            .ToListAsync(cancellationToken);

        if (direction == PaginationDirection.Backward)
            result.Reverse();

        return result;

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

    public async Task<IReadOnlyList<Permit>> SearchByDateAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Permits.Where(p => p.StartDate >= startDate && p.EndDate <= endDate)
                                       .AsNoTracking()
                                        .ToListAsync(cancellationToken: cancellationToken);
    }
}
