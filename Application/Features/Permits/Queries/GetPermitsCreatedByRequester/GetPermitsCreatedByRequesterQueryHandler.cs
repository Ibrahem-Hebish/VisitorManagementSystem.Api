using Application.Dtos.Permits;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Queries.GetPermitsCreatedByRequester;

public sealed class GetPermitsCreatedByRequesterQueryHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitsCreatedByRequesterQuery, Response<List<PermitDto>>>
{
    public async Task<Response<List<PermitDto>>> Handle(GetPermitsCreatedByRequesterQuery request, CancellationToken cancellationToken)
    {
        var permits = await permitQueryRepository.GetPermitsCreatedByRequester(new UserId(new Guid(request.Id)), cancellationToken);

        if (permits is null || permits.Count == 0)
            return NotFound<List<PermitDto>>("There is no permits.");

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}

