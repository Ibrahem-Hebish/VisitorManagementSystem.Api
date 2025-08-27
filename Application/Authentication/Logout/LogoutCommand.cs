namespace Application.Authentication.Logout;

public sealed record LogoutCommand : IRequest<Response<string>> { }
