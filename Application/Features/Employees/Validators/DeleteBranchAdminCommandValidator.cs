using Application.Features.Employees.DeleteBranchAdmin;

namespace Application.Features.Employees.Validators;

public sealed class DeleteBranchAdminCommandValidator : AbstractValidator<DeleteBranchAdminCommand>
{
    public DeleteBranchAdminCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Building Id is required.")
           .Must(BeAValidGuid).WithMessage("Building Id must be a valid GUID.");
    }

    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}