using Application.Dtos.PermitUpdateRequests;
using Domain.TenantDomain.PermitUpdateRequests.Repositories;

namespace Application.Features.PermitUpdateRequests.GetAll;

public sealed class GetAllPermitUpdateRequestsQueryHandler(
    IPermitUpdateRequestQueryRepository permitUpdateRequestQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllPermitUpdateRequestsQuery, Response<List<GetPermitUpdateRequestDto>>>
{
    public async Task<Response<List<GetPermitUpdateRequestDto>>> Handle(GetAllPermitUpdateRequestsQuery request, CancellationToken cancellationToken)
    {
        var permitUpdateRequests = await permitUpdateRequestQueryRepository.GetAllAsync();

        if (permitUpdateRequests is null || permitUpdateRequests.Count == 0)
            return NotFound<List<GetPermitUpdateRequestDto>>();

        var dtos = mapper.Map<List<GetPermitUpdateRequestDto>>(permitUpdateRequests);

        return Success(dtos);
    }
}
