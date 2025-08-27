using Application.BranchAdmin.CreateSecurity;

namespace Application.BranchAdmin.Validators;

public class CreateSecurityCommandValidator : AbstractValidator<CreateSecurityCommand>
{
    public CreateSecurityCommandValidator(IValidator<CreateBranchAdminCommand> baseValidator)
    {
        Include(baseValidator);
    }
}
