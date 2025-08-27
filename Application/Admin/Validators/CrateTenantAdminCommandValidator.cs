using Application.Admin.CreateTenantAdmin;

namespace Application.Admin.Validators;

public class CrateTenantAdminCommandValidator : AbstractValidator<CreateBranchAdminCommand>
{
    public CrateTenantAdminCommandValidator()
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
            .NotEmpty()
            .WithMessage("Password can not be empty")
            .MinimumLength(6)
            .WithMessage("Password can not be less than 6 chars.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Confirmed password must match password.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name can not be empty.");

        RuleFor(x => x.LastName)
           .NotEmpty()
           .WithMessage("First name can not be empty.");
    }
}


