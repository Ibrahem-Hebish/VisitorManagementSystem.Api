using Application.Users.Dtos;

namespace Application.Features.Authentication.GetProfile;

public record GetUserInfoQuery(string UserId) : IRequest<Response<GetUserDto>> { }
