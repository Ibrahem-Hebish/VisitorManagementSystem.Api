using Application.Features.Employees.CreateEmployee;

namespace Application.Features.Employees.CreateSecurity;
public record CreateSecurityCommand(CreateEmployeeCommand Command) : IRequest<Response<string>>, IValidatorRequest
{
}

