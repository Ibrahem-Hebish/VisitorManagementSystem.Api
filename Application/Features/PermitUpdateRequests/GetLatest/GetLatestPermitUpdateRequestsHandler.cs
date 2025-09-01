using Application.Dtos.PermitUpdateRequests;
using Domain.TenantDomain.PermitUpdateRequests.Repositories;

namespace Application.Features.PermitUpdateRequests.GetLatest;

public sealed class GetLatestPermitUpdateRequestsHandler(
    IPermitUpdateRequestQueryRepository permitUpdateRequestQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetLatestPermitUpdateRequests, Response<List<GetPermitUpdateRequestDto>>>
{
    public async Task<Response<List<GetPermitUpdateRequestDto>>> Handle(GetLatestPermitUpdateRequests request, CancellationToken cancellationToken)
    {
        var permitUpdateRequests = await permitUpdateRequestQueryRepository.GetLatestAsync();

        if(permitUpdateRequests is null || permitUpdateRequests.Count == 0)
            return NotFound<List<GetPermitUpdateRequestDto>>();

        var dtos = mapper.Map<List<GetPermitUpdateRequestDto>>(permitUpdateRequests);

        return Success(dtos);
    }
}
