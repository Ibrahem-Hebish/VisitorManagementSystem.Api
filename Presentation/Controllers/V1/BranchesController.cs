using Application.Features.Brnches.CreateBranch;
using Application.Features.Brnches.DeleteBranch;
using Application.Features.Brnches.GetBranchDetails;

namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class BranchesController(ISender sender) : AppControllerBase
{

    [HttpGet("{id}")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetBranchDetailsQuery(id));

        return NewResponse(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,TenantAdmin")]
    [SkipTenantFilter]
    public async Task<IActionResult> Create([FromBody] CreateBranchCommand command)
    {
        var response = await sender.Send(command);

        return NewResponse(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "TenantAdmin")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await sender.Send(new DeleteBranchCommand(id));

        return NewResponse(result);
    }

    [HttpPost("admin")]
    [Authorize(Roles = "TenantAdmin")]
    public async Task<IActionResult> CreateBranchAdmin(CreateBranchAdminCommand command)
    {

        var response = await sender.Send(command);

        return NewResponse(response);
    }

}
