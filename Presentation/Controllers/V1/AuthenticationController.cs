using Application.Authentication.GetProfile;
using Application.Authentication.Logout;

namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class AuthenticationController(ISender sender) : AppControllerBase
{

    [HttpPost("signin")]
    [SkipTenantFilter]
    //[EnableRateLimiting("SignInLimit")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
    {
        var response = await sender.Send(command);

        return NewResponse(response);
    }

    [HttpPost("logout")]
    [Authorize]
    [SkipTenantFilter]

    public async Task<IActionResult> LogOut()
    {
        var result = await sender.Send(new LogoutCommand());

        return NewResponse(result);
    }

    [HttpPost("refreshtoken")]
    [SkipTenantFilter]
    //[EnableRateLimiting("SignInLimit")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await sender.Send(new RefreshTokenCommand());

        return NewResponse(response);
    }

    [HttpGet("Profile/{id}")]
    [Authorize]
    [SkipTenantFilter]
    public async Task<IActionResult> Profile(string id)
    {
        var result = await sender.Send(new GetUserInfoQuery(id));

        return NewResponse(result);
    }
}
