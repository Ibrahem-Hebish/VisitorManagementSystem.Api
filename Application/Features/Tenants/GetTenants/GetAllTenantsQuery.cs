using Application.Dtos.Tenant;

namespace Application.Features.Tenants.GetTenants;

public record GetAllTenantsQuery : IRequest<Response<List<TenantDto>>> { }
