using Application.Features.Employees.CreateEmployee;

namespace Application.Features.Employees.CreateRequester;
public record CreateRequesterCommand(CreateEmployeeCommand Command) : IRequest<Response<string>>, IValidatorRequest
{
}

