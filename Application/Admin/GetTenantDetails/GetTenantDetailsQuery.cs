using Application.Dtos.Tenant;

namespace Application.Admin.GetTenantDetails;

public record GetTenantDetailsQuery(string TenantId) : IRequest<Response<TenantDetailsDto>> { }
