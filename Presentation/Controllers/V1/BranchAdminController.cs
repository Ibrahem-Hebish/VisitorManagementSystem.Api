using Application.BranchAdmin.CreateManager;
using Application.BranchAdmin.CreateRequester;
using Application.BranchAdmin.CreateSecurity;
using Application.BranchAdmin.DeleteManager;
using Application.BranchAdmin.DeleteRequester;
using Application.BranchAdmin.DeleteSecurity;

namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
[Authorize(Roles = "BranchAdmin")]
public class BranchAdminController(ISender sender) : AppControllerBase
{

    [HttpPost("createrequester")]
    public async Task<IActionResult> CreateRequester(CreateRequesterCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPost("createmanager")]
    public async Task<IActionResult> CreateManager(CreateManagerCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPost("createsecurity")]
    public async Task<IActionResult> CreateSecurity(CreateSecurityCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpDelete("deletemanager/{id}")]
    public async Task<IActionResult> DeleteManager(string id)
    {
        var result = await sender.Send(new DeleteManagerCommand(id));

        return NewResponse(result);
    }

    [HttpDelete("deleterequester/{id}")]
    public async Task<IActionResult> DeleteRequester(string id)
    {
        var result = await sender.Send(new DeleteRequesterCommand(id));

        return NewResponse(result);
    }

    [HttpDelete("deletesecurity/{id}")]
    public async Task<IActionResult> DeleteSecurity(string id)
    {
        var result = await sender.Send(new DeleteSecurityCommand(id));

        return NewResponse(result);
    }


}