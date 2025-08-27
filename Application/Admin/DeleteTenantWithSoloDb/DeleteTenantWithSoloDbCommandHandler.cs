
using Application.Services.TenantService;
using Domain.SharedTenantMetadataEntities.Repositories;
using Domain.SharedTenantMetadataEntities.Tenants.ObjectValues;
using Domain.Tenants.Repositories;

namespace Application.Admin.DeleteTenantWithSoloDb;

public class DeleteTenantWithSoloDbCommandHandler(
    ISharedTenantCommandRepository sharedTenantCommandRepository,
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    ITenantService tenantService,
    IPublisher publisher
    )
    : ResponseHandler,
    IRequestHandler<DeleteTenantWithSoloDbCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteTenantWithSoloDbCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await tenantService.DeleteDatabaseForTenant(publisher);

            try
            {
                var exsistedTenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(new Guid(request.TenantId)), cancellationToken);

                if (exsistedTenant is null)
                    return BadRequest<string>("There is no tenant with that id");

                await sharedTenantCommandRepository.DeleteAsync(exsistedTenant, cancellationToken);
            }
            catch
            {
                return InternalServerError<string>("Faild to delete tenant info from shared db.");
            }

            return Success("Tenant is deleted Succssefully");
        }
        catch
        {
            return InternalServerError<string>("Something went wrong try again later");
        }
    }
}
