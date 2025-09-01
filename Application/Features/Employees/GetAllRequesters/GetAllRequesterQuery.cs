using Application.Users.Dtos;

namespace Application.Features.Employees.GetAllRequesters;

public sealed record GetAllRequesterQuery : IRequest<Response<List<GetUserDto>>>;


