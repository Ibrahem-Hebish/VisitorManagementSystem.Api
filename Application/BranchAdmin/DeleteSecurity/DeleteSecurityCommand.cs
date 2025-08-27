using Domain.Users.Repositories.Requesters;

namespace Application.BranchAdmin.DeleteSecurity;

public sealed record DeleteSecurityCommand(string Id) : IRequest<Response<string>> { }
