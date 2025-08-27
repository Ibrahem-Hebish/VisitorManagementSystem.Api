using Application.Dtos.Tenant;
using Application.Services.EncryptionService;
using Domain.Tenants.Repositories;

namespace Application.Admin.GetTenants;

public class GetAllTenantsQueryhandler(
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    IConnectionStringProtector connectionStringProtector,
    IConfiguration configuration,
    IMapper mapper
    )
    : ResponseHandler,
    IRequestHandler<GetAllTenantsQuery, Response<List<TenantDto>>>
{
    public async Task<Response<List<TenantDto>>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
    {
        var tenants = await sharedTenantQueryRepository.GetAllAsync(cancellationToken);

        if (tenants.Count == 0)
            return NotFouned<List<TenantDto>>("There is no tenants");

        var tenantsDtos = mapper.Map<List<TenantDto>>(tenants);

        var sharedonnctionString = configuration.GetSection("TenantConnection").Value;

        foreach (var tenant in tenants)
        {
            var tenantDto = tenantsDtos.Find(x => x.Name == tenant.Name);

            if (connectionStringProtector.Decrypt(tenant.ConnectionString) == sharedonnctionString)
                tenantDto!.HasSoloDb = false;

            else
                tenantDto!.HasSoloDb = true;
        }

        return Success(tenantsDtos);
    }
}
