using Application.Admin.DeleteBranch;
using Application.Admin.DeleteTenantWithSoloDb;
using Application.Admin.GetBranchDetails;
using Application.Admin.GetTenantDetails;

namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
[Authorize(Roles = "Admin")]
public class AdminController(ISender sender) : AppControllerBase
{
    [HttpGet("GetTenants")]
    [SkipTenantFilter]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllTenantsQuery());

        return NewResponse(result);
    }

    [HttpGet("TenantDetails/{id}")]
    [SkipTenantFilter]
    public async Task<IActionResult> GetDetails(string id)
    {
        var result = await sender.Send(new GetTenantDetailsQuery(id));

        return NewResponse(result);
    }

    [HttpPost("CreateTenantWithSoloDatabase")]
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
    [SkipTenantFilter]
    public async Task<IActionResult> CreateTenantWithShredDb([Required] string tenantName)
    {
        var response = await sender.Send(new CreateTenantWithSharedDatabase(tenantName));

        return NewResponse(response);
    }

    [HttpDelete("DeleteTenantWithSharedDb")]
    public async Task<IActionResult> DeleteTenantWithSharedDb([FromHeader(Name = "TenantId")] string tenantId)
    {

        var response = await sender.Send(new DeleteTenantWithSharedDbCommand(tenantId));

        return NewResponse(response);
    }

    [HttpDelete("DeleteTenantWithSoloDb")]
    public async Task<IActionResult> DeleteTenantWithSoloDb([FromHeader(Name = "TenantId")] string tenantId)
    {

        var response = await sender.Send(new DeleteTenantWithSoloDbCommand(tenantId));

        return NewResponse(response);
    }

    [HttpGet("Branch/{id}")]
    public async Task<IActionResult> GetBranch(string id)
    {
        var result = await sender.Send(new GetBranchDetailsQuery(id));

        return NewResponse(result);
    }

    [HttpPost("CreateNewBranch")]
    public async Task<IActionResult> CreateNewBranch([FromBody] CreateBranchCommand command)
    {
        var response = await sender.Send(command);

        return NewResponse(response);
    }

    [HttpDelete("DeleteBranch/{id}")]
    public async Task<IActionResult> DeleteBranch(string id)
    {
        var result = await sender.Send(new DeleteBranchCommand(id));

        return NewResponse(result);
    }

    [HttpPost("CreateBranchAdmin")]
    public async Task<IActionResult> CreateTenantAdmin(
                            [FromBody] CreateBranchAdminCommand command,
                            [FromHeader(Name = "TenantId")] string tenantId,
                            [FromHeader(Name = "BranchId")] string branchId)
    {

        ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
        ArgumentNullException.ThrowIfNull(branchId, nameof(branchId));

        var response = await sender.Send(command);

        return NewResponse(response);
    }

}
