using Application.Features.Authentication.SendCodeToReseatPassword;

namespace Application.Features.Authentication.Validators;

public class SendCodeToResetPasswordValidator : AbstractValidator<SendCodeToReseatPasswordCommand>
{
    public SendCodeToResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email can not be empty.")
            .NotNull()
            .WithMessage("Email can not be null.")
            .EmailAddress()
            .WithMessage("invalid Email.");
    }
}