namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class AuthenticationController(ISender sender) : AppControllerBase
{

    [HttpPost("signin")]
    [SkipTenantFilter]
    [EnableRateLimiting("SignInLimit")]
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
    public async Task<IActionResult> RefreshToken()
    {
        var response = await sender.Send(new RefreshTokenCommand());

        return NewResponse(response);
    }

    [HttpPost("forgot-password")]
    [SkipTenantFilter]
    public async Task<IActionResult> ForgotPassword(SendCodeToReseatPasswordCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPost("reset-password")]
    [SkipTenantFilter]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPost("change-password")]
    [SkipTenantFilter]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
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
