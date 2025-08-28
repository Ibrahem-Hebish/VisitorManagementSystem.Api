using Domain.EntryLogs;

namespace Domain.Permits.Repositories;

public interface IPermitQueryRepository
{
    Task<Permit?> GetByIdAsync(PermitId id, CancellationToken cancellationToken = default);
    Task<Permit?> GetDetailsAsync(PermitId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permit>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permit>> GetPermitsHandledByManager(UserId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permit>> GetPermitsCreatedByRequester(UserId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permit>> GetPagedAsync(PermitId? id, DateTime? cursorDate, PaginationDirection direction, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permit>> GetLatestAsync(int count = 10, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PermitTrack>> GetTracksAsync(PermitId id, CancellationToken cancellationToken = default);
    Task<Entrylog?> GetEntryLogAsync(PermitId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permit>> SearchByDateAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

}
