using Application.Dtos.EntryLogs;

namespace Application.Features.Permits.Queries.GetPermitEntryLog;

public sealed record GetPermitEntryLogQuery(string PermitId) : IRequest<Response<EntryLogDto>>;
