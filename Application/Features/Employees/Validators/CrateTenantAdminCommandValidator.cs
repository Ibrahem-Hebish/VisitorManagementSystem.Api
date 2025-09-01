namespace Application.Features.Employees.Validators;

public class CrateTenantAdminCommandValidator : AbstractValidator<CreateTenantAdminCommand>
{
    public CrateTenantAdminCommandValidator(IValidator<CreateBranchAdminCommand> validator)
    {
        Include(validator);
    }
}