using Application.Features.Employees.DeleteEmployee;

namespace Application.Features.Employees.Validators;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Building Id is required.")
           .Must(BeAValidGuid).WithMessage("Building Id must be a valid GUID.");

        RuleFor(x => x.Position)
           .IsInEnum()
           .WithMessage("Invalid value")
           .NotNull()
           .WithMessage("Position can not be null");

    }
    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}
