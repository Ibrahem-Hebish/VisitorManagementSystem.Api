namespace Application.Admin.CreateTenantWithSharedDb;

public record CreateTenantWithSharedDatabase(string TenantName) : IRequest<Response<string>>
{
}
