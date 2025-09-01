using Domain.TenantDomain.PermitUpdateRequests.ObjectValues;
using Domain.TenantDomain.PermitUpdateRequests.Repositories;
using Domain.TenantDomain.Users.ObjectValues;

namespace Application.Features.PermitUpdateRequests.Update;

public sealed class UpdatePermitUpdateRequestCommandHandler(
    IPermitUpdateRequestQueryRepository permitUpdateRequestQueryRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<UpdatePermitUpdateRequestCommand, Response<string>>
{
    public async Task<Response<string>> Handle(UpdatePermitUpdateRequestCommand request, CancellationToken cancellationToken)
    {
        var id = new PermitUpdateRequestId(new Guid(request.Id));

        var permitUpdateRequest = await permitUpdateRequestQueryRepository.GetByIdAsync(id);

        if (permitUpdateRequest is null)
            return NotFound<string>();

        var requesterId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        permitUpdateRequest.Update(request.Action, request.Description, new UserId(new Guid(requesterId!)));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Success("Updated Successfully.");
    }
}
