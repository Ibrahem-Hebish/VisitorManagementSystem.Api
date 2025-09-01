namespace Application.Features.Authentication.ChangePassword;

public sealed record ChangePasswordCommand(string Password, string NewPassword, string ConfirmPassword) : IRequest<Response<string>>, IValidatorRequest;
