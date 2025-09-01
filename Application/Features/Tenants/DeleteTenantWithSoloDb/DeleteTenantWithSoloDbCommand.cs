namespace Application.Features.Tenants.DeleteTenantWithSoloDb;

public record DeleteTenantWithSoloDbCommand(string TenantId) : IRequest<Response<string>> { }
