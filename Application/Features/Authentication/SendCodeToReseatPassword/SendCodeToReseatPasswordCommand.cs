
namespace Application.Features.Authentication.SendCodeToReseatPassword;

public sealed record SendCodeToReseatPasswordCommand(string Email) : IRequest<Response<string>>, IValidatorRequest;
