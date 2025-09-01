namespace Application.Features.Employees.Validators;

public class CrateBranchAdminCommandValidator : AbstractValidator<CreateBranchAdminCommand>
{
    public CrateBranchAdminCommandValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email can not be empty.")
            .EmailAddress()
            .WithMessage("Invalid email.");

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .WithMessage("Phone Can not be null")
            .NotEmpty()
            .WithMessage("Phone can not be empty.");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .NotNull();

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("New password is too long.")
            .Matches(@"(?=.*[a-z])").WithMessage("New password must contain at least one lowercase letter.")
            .Matches(@"(?=.*[A-Z])").WithMessage("New password must contain at least one uppercase letter.")
            .Matches(@"(?=.*\d)").WithMessage("New password must contain at least one digit.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("Confirm password does not match new password.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name can not be empty.");

        RuleFor(x => x.LastName)
           .NotEmpty()
           .WithMessage("First name can not be empty.");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Invalid value")
            .NotNull()
            .WithMessage("Position can not be null");
    }
}
