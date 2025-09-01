using Application.Users.Dtos;

namespace Application.Features.Employees.GetEmployeeById;

public sealed record GetEmployeeByIdQuery(string Id) : IRequest<Response<GetUserDto>>, IValidatorRequest;


