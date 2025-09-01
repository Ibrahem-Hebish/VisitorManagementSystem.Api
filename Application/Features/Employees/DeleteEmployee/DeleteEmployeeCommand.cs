using Domain.TenantDomain.Users.Enums;

namespace Application.Features.Employees.DeleteEmployee;

public sealed record DeleteEmployeeCommand(string Id, EmployeePosition Position) : IRequest<Response<string>>, IValidatorRequest;
