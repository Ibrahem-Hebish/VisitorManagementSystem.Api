using Application.Dtos.PermitUpdateRequests;
using Domain.TenantDomain.PermitUpdateRequests.Repositories;

namespace Application.Features.PermitUpdateRequests.GetByRequesterId;

public sealed class GetPermitUpdateRequestByRequesterIdQueryHandler(
    IPermitUpdateRequestQueryRepository permitUpdateRequestQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitUpdateRequestByRequesterIdQuery, Response<List<GetPermitUpdateRequestDto>>>
{
    public async Task<Response<List<GetPermitUpdateRequestDto>>> Handle(GetPermitUpdateRequestByRequesterIdQuery request, CancellationToken cancellationToken)
    {
        var userId = new UserId(new Guid(request.ReqesterId));

        var permitUpdateRequests = await permitUpdateRequestQueryRepository.GetByRequesterId(userId);

        if (permitUpdateRequests is null || permitUpdateRequests.Count == 0)
            return NotFound<List<GetPermitUpdateRequestDto>>();

        var dtos = mapper.Map<List<GetPermitUpdateRequestDto>>(permitUpdateRequests);

        return Success(dtos);
    }
}
