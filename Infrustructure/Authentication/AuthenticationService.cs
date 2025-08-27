using Domain.Users.Repositories.Employees;
using Domain.Users.Repositories.Users;

namespace Infrustructure.Authentication;

public class AuthenticationService(
    IUserQueryRepository userQueryRepository,
    IEmployeeQueryRepository employeeQueryRepository,
    IBranchQueryRepository branchQueryRepository,
    IOptionsMonitor<JwtOptions> jwtOptions,
    IConfiguration configuration)

    : IAuthenticationService
{
    public async Task<(UserTokenDto, string, string)> CreateToken(User user,
        DateTime refreshTokenExpiredDate,
        string? refreshToken = null)
    {
        string accessToken;

        try
        {
            accessToken = await CreateAccessToken(user);
        }
        catch
        {
            throw new InvalidOperationException("Signing key is missing from configuration.");
        }

        UserTokenDto userToken = new()
        {
            AccessTokenExpiredDate = DateTime.UtcNow.AddMinutes(jwtOptions.CurrentValue!.LifeTime),
            RefreshTokenExpiredDate = refreshTokenExpiredDate,
            IsActive = true,
            UserId = user.Id.Id.ToString(),
            Role = (await userQueryRepository.GetUserRole(user.Id))!.Name
        };

        return (userToken, accessToken, refreshToken ?? Guid.NewGuid().ToString());
    }
    public async Task<TokenValidationResult> ValidateAccessToken(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var signingkey = configuration["jwtsigningkey"];
        var issuer = jwtOptions.CurrentValue.Issuer;
        var audience = jwtOptions.CurrentValue.Audience;

        if (string.IsNullOrWhiteSpace(signingkey))
            throw new InvalidOperationException("Signing key is missing from configuration.");

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = issuer ?? "jjjjjj",
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        signingkey!))
        };

        tokenValidationParameters.ValidIssuer = issuer;
        tokenValidationParameters.ValidAudience = audience;

        var result = await tokenHandler.ValidateTokenAsync(
            accessToken, tokenValidationParameters);

        return result;
    }
    private async Task<string> CreateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var signingkey = configuration["jwtsigningkey"];

        if (string.IsNullOrWhiteSpace(signingkey))
            throw new InvalidOperationException("Signing key is missing from configuration.");

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = jwtOptions.CurrentValue.Issuer,

            Audience = jwtOptions.CurrentValue.Audience,

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        signingkey!)),
                SecurityAlgorithms.HmacSha256),

            Subject = await GetClaims(user),

            Expires = DateTime.UtcNow.AddMinutes(jwtOptions.CurrentValue.LifeTime)

        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var accessToken = tokenHandler.WriteToken(securityToken);

        return accessToken;
    }
    private async Task<ClaimsIdentity> GetClaims(User user)
    {

        var role = await userQueryRepository.GetUserRole(user.Id);

        List<Claim> userClaims = [];

        if (role!.Name != "Admin")
        {
            var employee = await employeeQueryRepository.GetByIdAsync(user.Id) ??
                                   throw new InvalidOperationException("Employee not found for the user.");

            var branch = await branchQueryRepository.GetByIdAsync(employee.BranchId) ??
                                    throw new InvalidOperationException("Branch not found for the employee.");

            userClaims.Add(new("BranchId", employee.BranchId.Guid.ToString()));

            userClaims.Add(new("TenantId", branch.TenantId.Guid.ToString()));

        }

        userClaims.Add(new(ClaimTypes.NameIdentifier, user.Id.Id.ToString()));

        userClaims.Add(new Claim(ClaimTypes.Role, role.Name));


        var claimsIdentity = new ClaimsIdentity(userClaims);

        return claimsIdentity;
    }


}