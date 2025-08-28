using Application.Dtos.PermitTracks;
using Domain.Permits.ObjectValues;
using Domain.Permits.Repositories;

namespace Application.Permits.Queries.GetPermitHistory;

public sealed class GetPermitHistoryQueryHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitHistoryQuery, Response<IReadOnlyList<PermitTrackDto>>>
{
    public async Task<Response<IReadOnlyList<PermitTrackDto>>> Handle(GetPermitHistoryQuery request, CancellationToken cancellationToken)
    {
        var permitTracks = await permitQueryRepository.GetTracksAsync(new PermitId(new Guid(request.PermitId)), cancellationToken);

        if(permitTracks is null ||  permitTracks.Count() == 0)
            return NotFouned<IReadOnlyList<PermitTrackDto>>();

        var dtos = mapper.Map<IReadOnlyList<PermitTrackDto>>(permitTracks);

        return Success(dtos);
    }
}
