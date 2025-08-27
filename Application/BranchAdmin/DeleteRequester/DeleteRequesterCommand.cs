namespace Application.BranchAdmin.DeleteRequester;

public sealed record DeleteRequesterCommand(string Id) : IRequest<Response<string>> { }
