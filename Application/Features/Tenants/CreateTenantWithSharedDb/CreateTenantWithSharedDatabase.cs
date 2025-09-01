namespace Application.Features.Tenants.CreateTenantWithSharedDb;

public record CreateTenantWithSharedDatabase(string TenantName) : IRequest<Response<string>>
{
}
