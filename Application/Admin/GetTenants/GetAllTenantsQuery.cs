using Application.Dtos.Tenant;

namespace Application.Admin.GetTenants;

public record GetAllTenantsQuery : IRequest<Response<List<TenantDto>>> { }
