using Application.Users.Dtos;

namespace Application.Features.Employees.GetAllSecurities;

public sealed record GetAllSecuritiesQuery : IRequest<Response<List<GetUserDto>>>;


