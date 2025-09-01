using Application.Features.Employees.CreateEmployee;

namespace Application.Features.Employees.CreateManager;
public record CreateManagerCommand(CreateEmployeeCommand Command) : IRequest<Response<string>>, IValidatorRequest
{
}

