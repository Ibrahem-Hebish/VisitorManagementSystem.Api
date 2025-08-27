namespace Application.BranchAdmin.DeleteManager;

public sealed record DeleteManagerCommand(string Id) : IRequest<Response<string>> { }
