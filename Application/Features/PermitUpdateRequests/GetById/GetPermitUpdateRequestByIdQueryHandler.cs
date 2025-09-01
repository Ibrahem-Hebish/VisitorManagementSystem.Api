using Application.Dtos.PermitUpdateRequests;
using Domain.TenantDomain.PermitUpdateRequests.ObjectValues;
using Domain.TenantDomain.PermitUpdateRequests.Repositories;

namespace Application.Features.PermitUpdateRequests.GetById;

public sealed class GetPermitUpdateRequestByIdQueryHandler(
    IPermitUpdateRequestQueryRepository permitUpdateRequestQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitUpdateRequestByIdQuery, Response<GetPermitUpdateRequestDto>>
{
    public async Task<Response<GetPermitUpdateRequestDto>> Handle(GetPermitUpdateRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new PermitUpdateRequestId(new Guid(request.Id));

        var permitUpdateRequest = await permitUpdateRequestQueryRepository.GetByIdAsync(id);

        if (permitUpdateRequest is null)
            return NotFound<GetPermitUpdateRequestDto>();

        var dto = mapper.Map<GetPermitUpdateRequestDto>(permitUpdateRequest);

        return Success(dto);
    }
}
