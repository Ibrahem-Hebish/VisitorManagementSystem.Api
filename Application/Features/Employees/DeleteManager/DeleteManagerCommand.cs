namespace Application.Features.Employees.DeleteManager;

public sealed record DeleteManagerCommand(string Id) : IRequest<Response<string>> { }
