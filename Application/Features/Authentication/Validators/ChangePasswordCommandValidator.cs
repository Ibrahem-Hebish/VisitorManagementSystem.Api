using Application.Features.Authentication.ChangePassword;

namespace Application.Features.Authentication.Validators;

public abstract class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    protected ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Current password is required.")
            .WithMessage("Current password is invalid.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("New password is too long.")
            .Matches(@"(?=.*[a-z])").WithMessage("New password must contain at least one lowercase letter.")
            .Matches(@"(?=.*[A-Z])").WithMessage("New password must contain at least one uppercase letter.")
            .Matches(@"(?=.*\d)").WithMessage("New password must contain at least one digit.")
            .NotEqual(x => x.Password).WithMessage("New password must be different from the current password.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.NewPassword).WithMessage("Confirm password does not match new password.");
    }
}
