namespace Application.Features.Authentication.Validators;

public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("New password is too long.")
            .Matches(@"(?=.*[a-z])").WithMessage("New password must contain at least one lowercase letter.")
            .Matches(@"(?=.*[A-Z])").WithMessage("New password must contain at least one uppercase letter.")
            .Matches(@"(?=.*\d)").WithMessage("New password must contain at least one digit.");

    }
}
