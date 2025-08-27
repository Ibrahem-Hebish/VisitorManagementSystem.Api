using Application.CustomResponse;
using Application.Dtos.UserToken;
using Application.Validation;
using MediatR;

namespace Application.Authentication.SignIn;

public sealed record SignInCommand(string Email, string Password) :

    IRequest<Response<UserTokenDto>>, IValidatorRequest
{
}
