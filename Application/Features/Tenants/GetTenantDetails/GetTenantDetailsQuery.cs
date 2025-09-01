using Application.Dtos.Tenant;

namespace Application.Features.Tenants.GetTenantDetails;

public record GetTenantDetailsQuery(string TenantId) : IRequest<Response<TenantDetailsDto>> { }
