namespace Application.Admin.DeleteTenantWithSoloDb;

public record DeleteTenantWithSoloDbCommand(string TenantId) : IRequest<Response<string>> { }
