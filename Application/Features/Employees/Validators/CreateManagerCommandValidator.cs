using Application.Features.Employees.CreateManager;

namespace Application.Features.Employees.Validators;

public class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
{
    public CreateManagerCommandValidator()
    {
        RuleFor(x => x.Command.Email)
           .NotEmpty()
           .WithMessage("Email can not be empty.")
           .EmailAddress()
           .WithMessage("Invalid email.");

        RuleFor(x => x.Command.PhoneNumber)
            .NotNull()
            .WithMessage("Phone Can not be null")
            .NotEmpty()
            .WithMessage("Phone can not be empty.");

        RuleFor(x => x.Command.Gender)
            .IsInEnum()
            .NotNull();

        RuleFor(x => x.Command.Password)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("New password is too long.")
            .Matches(@"(?=.*[a-z])").WithMessage("New password must contain at least one lowercase letter.")
            .Matches(@"(?=.*[A-Z])").WithMessage("New password must contain at least one uppercase letter.")
            .Matches(@"(?=.*\d)").WithMessage("New password must contain at least one digit.");

        RuleFor(x => x.Command.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Command.Password).WithMessage("Confirm password does not match new password.");

        RuleFor(x => x.Command.FirstName)
            .NotEmpty()
            .WithMessage("First name can not be empty.");

        RuleFor(x => x.Command.LastName)
           .NotEmpty()
           .WithMessage("First name can not be empty.");

        RuleFor(x => x.Command.Position)
            .IsInEnum()
            .WithMessage("Invalid value")
            .NotNull()
            .WithMessage("Position can not be null");

        RuleFor(x => x.Command.Gender)
            .IsInEnum()
            .WithMessage("Invalid value")
            .NotNull()
            .WithMessage("Position can not be null");
    }
}
