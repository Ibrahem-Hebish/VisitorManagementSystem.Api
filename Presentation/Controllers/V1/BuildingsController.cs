namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
[Authorize(Roles = "BranchAdmin")]
public class BuildingsController(ISender sender) : AppControllerBase
{
    [HttpGet]
    [Authorize(Roles = "TenantAdmin,BranchAdmin")]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllBuildingQuery());

        return NewResponse(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetBuildingByIdQuery(id));

        return NewResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBuildingCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBuildingCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await sender.Send(new DeleteBuildingCommand(id));

        return NewResponse(result);
    }
}