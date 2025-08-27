using Application.Dtos.Tenant;
using Domain.SharedTenantMetadataEntities.Tenants.ObjectValues;
using Domain.Tenants.Repositories;

namespace Application.Admin.GetTenantDetails;

public class GetTenantDetilsHandler(
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    IMapper mapper)
    : ResponseHandler,
    IRequestHandler<GetTenantDetailsQuery, Response<TenantDetailsDto>>
{
    public async Task<Response<TenantDetailsDto>> Handle(GetTenantDetailsQuery request, CancellationToken cancellationToken)
    {

        var tenant = await sharedTenantQueryRepository.GetByIdWithDetailsAsync(new SharedTenantId(new Guid(request.TenantId)), cancellationToken);

        if (tenant is null)
            return BadRequest<TenantDetailsDto>("There is no tenant with this id.");


        var tenantDto = mapper.Map<TenantDetailsDto>(tenant);

        return Success(tenantDto);
    }
}
