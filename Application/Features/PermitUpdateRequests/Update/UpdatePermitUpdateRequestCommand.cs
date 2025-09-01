using Domain.TenantDomain.PermitUpdateRequests.Enums;

namespace Application.Features.PermitUpdateRequests.Update;

public sealed record UpdatePermitUpdateRequestCommand(
    string Id,
    PermitUpdateAction Action,
    string Description) : IRequest<Response<string>>, IValidatorRequest;
