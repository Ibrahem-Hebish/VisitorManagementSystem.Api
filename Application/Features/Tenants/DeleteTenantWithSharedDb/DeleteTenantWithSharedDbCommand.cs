namespace Application.Features.Tenants.DeleteTenantWithSharedDb;

public record DeleteTenantWithSharedDbCommand(string TenantId) : IRequest<Response<string>> { }
