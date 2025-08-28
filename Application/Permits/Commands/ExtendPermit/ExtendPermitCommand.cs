namespace Application.Permits.Commands.ExtendPermit;

public sealed record ExtendPermitCommand(string PermitId, DateTime NewEndDate) : IRequest<Response<string>>, IValidatorRequest;
