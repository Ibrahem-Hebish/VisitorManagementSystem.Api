using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.Permits.Repositories;
using Domain.TenantDomain.PermitUpdateRequests;
using Domain.TenantDomain.Users.ObjectValues;
using Serilog;

namespace Application.Features.PermitUpdateRequests.Create;

public sealed class CreatePermitUpdateRequestCommandHandler(
    IPermitQueryRepository permitQueryRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<CreatePermitUpdateRequestCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreatePermitUpdateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var id = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var permitId = new PermitId(new Guid(request.PermitId));

            var permit = await permitQueryRepository.GetByIdAsync(permitId, cancellationToken);

            if (permit is null)
                return NotFound<string>("There is no permit with this id.");

            var requesterId = new UserId(new Guid(id));

            var permitUpdateRequest = PermitUpdateRequest.Create(request.Action, permitId, requesterId, request.Description);

            permit.AddPermitUpdateRequest(permitUpdateRequest);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Created<string>("Created Successfully.");

        }
        catch (Exception ex)
        {
            Log.Error($"Error while createing permit update request for permit with id {request.PermitId}. message: {ex.Message}");

            return InternalServerError<string>();
        }
    }
}
