using Application.Dtos.EntryLogs;
using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Queries.GetPermitEntryLog;

public sealed class GetPermitEntryLogQueryHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitEntryLogQuery, Response<EntryLogDto>>
{
    public async Task<Response<EntryLogDto>> Handle(GetPermitEntryLogQuery request, CancellationToken cancellationToken)
    {
        var entryLog = await permitQueryRepository.GetEntryLogAsync(new PermitId(new Guid(request.PermitId)), cancellationToken);

        if (entryLog is null)
            return NotFound<EntryLogDto>();

        var dto = mapper.Map<EntryLogDto>(entryLog);

        return Success(dto);
    }
}
