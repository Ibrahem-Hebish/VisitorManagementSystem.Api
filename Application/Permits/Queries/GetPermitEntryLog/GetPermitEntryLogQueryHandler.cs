using Application.Dtos.EntryLogs;
using Domain.Permits.ObjectValues;
using Domain.Permits.Repositories;

namespace Application.Permits.Queries.GetPermitEntryLog;

public sealed class GetPermitEntryLogQueryHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitEntryLogQuery, Response<EntryLogDto>>
{
    public async Task<Response<EntryLogDto>> Handle(GetPermitEntryLogQuery request, CancellationToken cancellationToken)
    {
        var entryLog = await permitQueryRepository.GetEntryLogAsync(new PermitId(new Guid(request.PermitId)), cancellationToken);

        if(entryLog is null)
            return NotFouned<EntryLogDto>();

        var dto = mapper.Map<EntryLogDto>(entryLog);

        return Success(dto);
    }
}
