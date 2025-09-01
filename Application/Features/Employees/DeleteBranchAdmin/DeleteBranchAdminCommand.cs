namespace Application.Features.Employees.DeleteBranchAdmin;

public sealed record DeleteBranchAdminCommand(string Id) : IRequest<Response<string>>, IValidatorRequest;
