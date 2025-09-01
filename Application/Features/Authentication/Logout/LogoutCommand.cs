namespace Application.Features.Authentication.Logout;

public sealed record LogoutCommand : IRequest<Response<string>> { }
