namespace Application.Features.Brnches.DeleteBranch;

public sealed record DeleteBranchCommand(string Id) : IRequest<Response<string>> { }
