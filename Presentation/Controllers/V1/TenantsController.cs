

using Application.Features.Tenants.CreateTenantDatabase;
using Application.Features.Tenants.CreateTenantWithSharedDb;
using Application.Features.Tenants.DeleteTenantWithSharedDb;
using Application.Features.Tenants.DeleteTenantWithSoloDb;
using Application.Features.Tenants.GetTenantDetails;
using Application.Features.Tenants.GetTenants;

namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class TenantsController(ISender sender) : AppControllerBase
{
    [HttpGet]
    [SkipTenantFilter]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllTenantsQuery());

        return NewResponse(result);
    }

    [HttpGet("Details/{id}")]
    [SkipTenantFilter]
    [Authorize(Roles = "Admin,TenantAdmin")]
    public async Task<IActionResult> GetDetails(string id)
    {
        var result = await sender.Send(new GetTenantDetailsQuery(id));

        return NewResponse(result);
    }

    [HttpPost("CreateTenantWithSoloDatabase")]
    [Authorize(Roles = "Admin")]
    [SkipTenantFilter]
    public async Task<IActionResult> CreateTenantDatabase(
        [FromHeader(Name = "TenantName")] string tenantName,
        [FromHeader(Name = "ConnectionString")] string connectionString)
    {
        ArgumentNullException.ThrowIfNull(tenantName, nameof(tenantName));
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

        var response = await sender.Send(new CreateTenantDatabaseCommand());

        return NewResponse(response);
    }

    [HttpPost("CreateTenantWithSharedDb")]
    [Authorize(Roles = "Admin")]
    [SkipTenantFilter]
    public async Task<IActionResult> CreateTenantWithShredDb([Required] string tenantName)
    {
        var response = await sender.Send(new CreateTenantWithSharedDatabase(tenantName));

        return NewResponse(response);
    }

    [HttpDelete("DeleteTenantWithSharedDb")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTenantWithSharedDb([FromHeader(Name = "TenantId")] string tenantId)
    {

        var response = await sender.Send(new DeleteTenantWithSharedDbCommand(tenantId));

        return NewResponse(response);
    }

    [HttpDelete("DeleteTenantWithSoloDb")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTenantWithSoloDb([FromHeader(Name = "TenantId")] string tenantId)
    {

        var response = await sender.Send(new DeleteTenantWithSoloDbCommand(tenantId));

        return NewResponse(response);

    }

    [HttpPost("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTenantAdmin(
        CreateTenantAdminCommand command,
        [FromHeader(Name = "BranchId")] string branchId)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

}