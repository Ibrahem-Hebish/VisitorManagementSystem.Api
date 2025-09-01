using Application.Features.Employees.GetEmployeeById;

namespace Application.Features.Employees.Validators;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
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
