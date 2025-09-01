using Application.Dtos.Permits;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Queries.GetPermitsHandedByManager;

public sealed class GetPermitsHandledByManagerQueryHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitsHandledByManagerQuery, Response<List<PermitDto>>>
{
    public async Task<Response<List<PermitDto>>> Handle(GetPermitsHandledByManagerQuery request, CancellationToken cancellationToken)
    {
        var permits = await permitQueryRepository.GetPermitsHandledByManager(new UserId(new Guid(request.Id)), cancellationToken);

        if (permits is null || permits.Count == 0)
            return NotFound<List<PermitDto>>("There is no permits.");

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}
