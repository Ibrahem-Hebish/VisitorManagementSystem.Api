namespace Application.Features.Permits.Commands.ApprovePermit;

public sealed record ApprovePermitCommand(string PermitId) : IRequest<Response<string>>, IValidatorRequest;
