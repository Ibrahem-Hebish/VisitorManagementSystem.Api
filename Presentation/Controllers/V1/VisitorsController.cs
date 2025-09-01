namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class VisitorsController(ISender sender) : AppControllerBase
{

    [HttpGet]
    [Authorize(Roles = "TenantAdmin,Requester,Manager,Security,BranchAdmin")]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllVisitorsQuery());

        return NewResponse(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "TenantAdmin,Requester,Manager,Security,BranchAdmin")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetVisitorByIdQuery(id));

        return NewResponse(result);
    }

    [HttpPost]
    [Authorize(Roles = "Requester")]
    public async Task<IActionResult> Create(CreateVisitorCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPut]
    [Authorize(Roles = "Requester")]
    public async Task<IActionResult> Update(UpdateVisitorCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpDelete("{id}")]
    [Authorize("Manager")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await sender.Send(new DeleteVisitorCommand(id));

        return NewResponse(result);
    }
}