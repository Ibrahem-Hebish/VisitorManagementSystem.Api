using Application.Dtos.EntryLogs;

namespace Application.Permits.Queries.GetPermitEntryLog;

public sealed record GetPermitEntryLogQuery(string PermitId) : IRequest<Response<EntryLogDto>>;
