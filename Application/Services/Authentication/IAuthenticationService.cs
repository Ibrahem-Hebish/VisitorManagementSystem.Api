using Application.Dtos.UserToken;
using Domain.TenantDomain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Authentication;

public interface IAuthenticationService
{
    public Task<(UserTokenDto, string, string)> CreateToken(User user, DateTime refreshTokenExpiredDate, string? refreshToken = null);
    public Task<TokenValidationResult> ValidateAccessToken(string accessToken);
}

