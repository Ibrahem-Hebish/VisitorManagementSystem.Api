namespace Application.Features.Employees.DeleteSecurity;

public sealed record DeleteSecurityCommand(string Id) : IRequest<Response<string>> { }
