using Application.Users.Dtos;

namespace Application.Features.Employees.GetAllEmployees;

public sealed record GetAllEmployeesQuery : IRequest<Response<List<GetUserDto>>>;


