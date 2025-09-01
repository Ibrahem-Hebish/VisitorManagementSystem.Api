using Application.Dtos.Permits;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Queries.GetAllPermits;

public class GetAllPermitsHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllPermitsQuery, Response<List<PermitDto>>>
{
    public async Task<Response<List<PermitDto>>> Handle(GetAllPermitsQuery request, CancellationToken cancellationToken)
    {
        var permits = await permitQueryRepository.GetAllAsync(cancellationToken);

        if (permits is null || permits.Count == 0)
            return NotFound<List<PermitDto>>("There is no permits.");

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}



