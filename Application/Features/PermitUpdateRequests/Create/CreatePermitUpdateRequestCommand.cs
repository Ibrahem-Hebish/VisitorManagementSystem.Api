using Domain.TenantDomain.PermitUpdateRequests.Enums;

namespace Application.Features.PermitUpdateRequests.Create;

public sealed record CreatePermitUpdateRequestCommand(
    string PermitId,
    PermitUpdateAction Action,
    string Description) : IRequest<Response<string>>, IValidatorRequest;



