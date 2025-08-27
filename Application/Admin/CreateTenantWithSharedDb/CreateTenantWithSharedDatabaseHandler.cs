using Application.Services.EncryptionService;
using Application.Services.TenantService;
using Application.Services.UnitOfWork;
using Domain.SharedTenantMetadataEntities.Repositories;
using Domain.SharedTenantMetadataEntities.Tenants;
using Domain.SharedTenantMetadataEntities.Tenants.ObjectValues;
using Domain.Tenants;
using Domain.Tenants.Repositories;

namespace Application.Admin.CreateTenantWithSharedDb;

public class CreateTenantWithSharedDatabaseHandler(
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    ITenantCommandRepository tenantCommandRepository,
    ISharedTenantCommandRepository sharedTenantCommandRepository,
    IConnectionStringProtector connectionStringProtector,
    ITenantService tenantService,
    IConfiguration configuration,
    IUnitOfWork unitOfWork
    )
    : ResponseHandler,
    IRequestHandler<CreateTenantWithSharedDatabase, Response<string>>
{
    public async Task<Response<string>> Handle(CreateTenantWithSharedDatabase request, CancellationToken cancellationToken)
    {
        var exsostedTenant = await sharedTenantQueryRepository.GetByNameAsync(request.TenantName, cancellationToken);

        if (exsostedTenant is not null)
            return BadRequest<string>("There is tenant with that name");

        var connectionString = configuration.GetSection("TenantConnection").Value;

        if (String.IsNullOrEmpty(connectionString))
            return InternalServerError<string>("Error while processing your request");

        tenantService.SetConnectionString(connectionString);

        connectionString = connectionStringProtector.Encrypt(connectionString);

        var tenant = Tenant.Create(request.TenantName, connectionString);

        await tenantCommandRepository.AddAsync(tenant, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var sharedTenant = SharedTenant.Create(request.TenantName, connectionString);

        sharedTenant.SetId(new SharedTenantId(tenant.Id.Guid));

        await sharedTenantCommandRepository.AddAsync(sharedTenant, cancellationToken);

        return Success<string>("Tenant added successfully");

    }
}
