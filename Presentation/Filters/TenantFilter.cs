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
        if (role == "BranchAdmin" || role == "Requester" || role == "Manager" || role == "Security")
        {
            var branchId = context.HttpContext.User.FindFirst("BranchId")?.Value;

            ArgumentNullException.ThrowIfNull(branchId, nameof(branchId));

            var branch = await sharedBranchQueryRepository.GetByIdAsync(new SharedBranchId(new Guid(branchId)));

            ArgumentNullException.ThrowIfNull(branch, nameof(branch));

            var tenant = await sharedTenantQueryRepository.GetByIdAsync(branch.TenantId);

            ArgumentNullException.ThrowIfNull(tenant, nameof(tenant));

            var connctionString = tenant.ConnectionString;

            connctionString = connectionStringProtector.Decrypt(connctionString);

            tenantService.SetConnectionString(connctionString);

            tenantService.SetBranchId(branchId);
        }

        //for admin
        else if (role == "Admin")
        {

            // load tenant id and name from header

            var tenantId = context.HttpContext.Request.Headers["TenantId"].ToString();
            // 2 go to shared db to get tenant name and id
            var tenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(new Guid(tenantId)));

            ArgumentNullException.ThrowIfNull(tenant, nameof(tenant));

            // 3 set tenant connectionString and name in tenant service

            var connectionString = tenant.ConnectionString;

            connectionString = connectionStringProtector.Decrypt(connectionString);

            tenantService.SetConnectionString(connectionString);

        }

        await next();

    }
}
