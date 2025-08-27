using Application.Services.EncryptionService;
using Application.Services.TenantService;
using Domain.SharedTenantMetadataEntities.Repositories;
using Domain.SharedTenantMetadataEntities.Tenants;
using Domain.SharedTenantMetadataEntities.Tenants.ObjectValues;
using Domain.Tenants;

namespace Application.Admin.CreateTenantDatabase;

public class CreateTenantDatabaseCommandHandler(
    ITenantService tenantService,
    IServiceProvider serviceProvider,
    ISharedTenantCommandRepository sharedTenantCommandRepository,
    IHttpContextAccessor httpContextAccessor,
    IConnectionStringProtector connectionStringProtector)
    : ResponseHandler, IRequestHandler<CreateTenantDatabaseCommand, Response<string>>
{

    public async Task<Response<string>> Handle(CreateTenantDatabaseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var connectionString = httpContextAccessor.HttpContext.Request.Headers
            ["ConnectionString"].ToString();


            if (string.IsNullOrEmpty(connectionString))
                return BadRequest<string>("Connection string is not provided in the request headers.");

            tenantService.SetConnectionString(connectionString);

            connectionString = connectionStringProtector.Encrypt(connectionString);

            var tenantName = httpContextAccessor.HttpContext.Request.Headers
                 ["TenantName"].ToString();

            if (string.IsNullOrEmpty(tenantName))
                return BadRequest<string>("Tenant name is not provided in the request headers.");
            var tenant = Tenant.Create(tenantName, connectionString);


            var (tenantId, tenantname) = await tenantService.CreateDatabaseForTenant(tenant, serviceProvider);

            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(tenantname))
            {
                return BadRequest<string>("Tenant ID or Tenant Name is null or empty.");
            }

            var sharedtenant = SharedTenant.Create(new SharedTenantId(Guid.Parse(tenantId)), tenantname, connectionString);

            await sharedTenantCommandRepository.AddAsync(sharedtenant, cancellationToken);

            return Success("Tenant database creation initiated successfully.");
        }
        catch (Exception ex)
        {
            return InternalServerError<string>($"An error occurred while creating tenant database: {ex.Message}");
        }
    }
}
