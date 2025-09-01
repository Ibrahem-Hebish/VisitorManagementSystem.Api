using Application.Dtos.Permits;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Queries.GetLatest;

public sealed class GetLatestPermitsQueryHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetLatestPermitsQuery, Response<List<PermitDto>>>
{
    public async Task<Response<List<PermitDto>>> Handle(GetLatestPermitsQuery request, CancellationToken cancellationToken)
    {
        var permits = await permitQueryRepository.GetLatestAsync(request.Count??10, cancellationToken);

        if (permits is null || permits.Count == 0)
            return NotFound<List<PermitDto>>();

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}


