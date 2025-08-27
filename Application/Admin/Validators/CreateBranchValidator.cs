using Application.Admin.CreateBranch;

namespace Application.Admin.Validators;



public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.BranchName)
            .NotEmpty()
            .WithMessage("Branch name is required.")
            .MinimumLength(3)
            .WithMessage("Branch name must be at least 3 characters long.")
            .MaximumLength(50)
            .WithMessage("Branch name must not exceed 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\d{1,14}$")
            .WithMessage("Phone number must be in a valid format (e.g., +1234567890).");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .MinimumLength(2)
            .WithMessage("Country must be at least 2 characters long.")
            .MaximumLength(50)
            .WithMessage("Country must not exceed 50 characters.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .MinimumLength(2)
            .WithMessage("City must be at least 2 characters long.")
            .MaximumLength(50)
            .WithMessage("City must not exceed 50 characters.");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street is required.")
            .MinimumLength(2)
            .WithMessage("Street must be at least 2 characters long.")
            .MaximumLength(100)
            .WithMessage("Street must not exceed 100 characters.");

    }
}


