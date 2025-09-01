namespace Application.Features.Authentication.ReseatPassword;

public sealed record ResetPasswordCommand(string Email, string Code, string NewPassword, string ConfirmPassword) : IRequest<Response<string>>, IValidatorRequest;
