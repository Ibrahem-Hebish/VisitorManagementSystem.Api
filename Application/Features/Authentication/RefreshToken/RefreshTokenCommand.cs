using Application.Dtos.UserToken;
using Application.Validation;

namespace Application.Features.Authentication.RefreshToken;

public record RefreshTokenCommand
    : IRequest<Response<UserTokenDto>>, IValidatorRequest
{ }
