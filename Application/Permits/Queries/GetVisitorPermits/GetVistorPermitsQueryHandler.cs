using Application.Dtos.Permits;
using Domain.Permits.Repositories;
using Domain.Visitors.ObjectValues;

namespace Application.Permits.Queries.GetVisitorPermits;

public sealed class GetVistorPermitsQueryHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetVistorPermitsQuery, Response<List<PermitDto>>>
{
    public async Task<Response<List<PermitDto>>> Handle(GetVistorPermitsQuery request, CancellationToken cancellationToken)
    {
        var permits = await permitQueryRepository.GetVisitorPermitsAsync(new VisitorId(new Guid(request.Id)), cancellationToken);

        if(permits is null || permits.Count == 0)
            return NotFouned<List<PermitDto>>();

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}

