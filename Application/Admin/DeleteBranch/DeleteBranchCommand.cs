namespace Application.Admin.DeleteBranch;

public sealed record DeleteBranchCommand(string Id) : IRequest<Response<string>> { }
