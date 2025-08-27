using Application.BranchAdmin.CreateManager;

namespace Application.BranchAdmin.Validators;

public class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
{
    public CreateManagerCommandValidator(IValidator<CreateBranchAdminCommand> baseValidator)
    {
        Include(baseValidator);
    }
}
