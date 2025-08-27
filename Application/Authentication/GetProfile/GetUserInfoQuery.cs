using Application.Users.Dtos;

namespace Application.Authentication.GetProfile;

public record GetUserInfoQuery(string UserId) : IRequest<Response<GetUserDto>> { }
