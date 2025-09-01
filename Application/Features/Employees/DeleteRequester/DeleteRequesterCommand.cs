namespace Application.Features.Employees.DeleteRequester;

public sealed record DeleteRequesterCommand(string Id) : IRequest<Response<string>> { }
