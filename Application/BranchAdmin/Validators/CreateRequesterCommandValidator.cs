using Application.BranchAdmin.CreateRequester;

namespace Application.BranchAdmin.Validators;

public class CreateRequesterCommandValidator : AbstractValidator<CreateRequesterCommand>
{
    public CreateRequesterCommandValidator(IValidator<CreateBranchAdminCommand> baseValidator)
    {
        Include(baseValidator);
    }
}
