using Domain.TenantDomain.Roles.Enums;

namespace Presentation.Filters;

public class TenantFilter(
    ISharedBranchQueryRepository sharedBranchQueryRepository,
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    ITenantService tenantService,
    IConnectionStringProtector connectionStringProtector

    ) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {


        var skipFilter = context.ActionDescriptor.EndpointMetadata
            .OfType<SkipTenantFilterAttribute>()
            .Any();

        if (skipFilter)
        {
            await next();
            return;
        }

        var role = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        ArgumentNullException.ThrowIfNull(role, nameof(role));

        // for employee 
        if (role == Roles.BranchAdmin.ToString() || role == Roles.Security.ToString() ||
            role == Roles.Manager.ToString() || role == Roles.Requester.ToString() ||
            role == Roles.TenantAdmin.ToString())
        {
            var branchId = context.HttpContext.User.FindFirstValue("BranchId");

            ArgumentNullException.ThrowIfNull(branchId, nameof(branchId));

            var branch = await sharedBranchQueryRepository.GetByIdAsync(new SharedBranchId(new Guid(branchId)));

            ArgumentNullException.ThrowIfNull(branch, nameof(branch));

            var tenant = await sharedTenantQueryRepository.GetByIdAsync(branch.TenantId);

            ArgumentNullException.ThrowIfNull(tenant, nameof(tenant));

            var connctionString = tenant.ConnectionString;

            connctionString = connectionStringProtector.Decrypt(connctionString);

            tenantService.SetConnectionString(connctionString);

            tenantService.SetBranchId(branchId);

            tenantService.SetTenantId(tenant.Id.Value.ToString());

        }

        else if (role == Roles.TenantAdmin.ToString())
        {
            var branchId = context.HttpContext.Request.Headers["BranchId"].ToString();

            ArgumentNullException.ThrowIfNull(branchId, nameof(branchId));

            var branch = await sharedBranchQueryRepository.GetByIdAsync(new SharedBranchId(new Guid(branchId)));

            ArgumentNullException.ThrowIfNull(branch, nameof(branch));

            var tenant = await sharedTenantQueryRepository.GetByIdAsync(branch.TenantId);

            ArgumentNullException.ThrowIfNull(tenant, nameof(tenant));

            var connctionString = tenant.ConnectionString;

            connctionString = connectionStringProtector.Decrypt(connctionString);

            tenantService.SetConnectionString(connctionString);

            tenantService.SetBranchId(branchId);

            tenantService.SetTenantId(tenant.Id.Value.ToString());
        }

        //for admin
        else if (role == Roles.Admin.ToString())
        {

            // load tenant id from header

            var tenantId = context.HttpContext.Request.Headers["TenantId"].ToString();
            // 2 go to shared db to get tenant name and id
            var tenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(new Guid(tenantId)));

            ArgumentNullException.ThrowIfNull(tenant, nameof(tenant));

            // 3 set tenant connectionString tenant service

            var connectionString = tenant.ConnectionString;

            connectionString = connectionStringProtector.Decrypt(connectionString);

            tenantService.SetConnectionString(connectionString);

            tenantService.SetTenantId(tenant.Id.Value.ToString());

        }

        await next();

    }
}
