namespace Application.Admin.DeleteTenantWithSharedDb;

public record DeleteTenantWithSharedDbCommand(string TenantId) : IRequest<Response<string>> { }
