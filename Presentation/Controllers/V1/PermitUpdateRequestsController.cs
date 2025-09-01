namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class PermitUpdateRequestsController(ISender sender) : AppControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Manager,BranchAdmin,TenantAdmin")]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllPermitUpdateRequestsQuery());

        return NewResponse(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,BranchAdmin,TenantAdmin")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetPermitUpdateRequestByIdQuery(id));

        return NewResponse(result);
    }

    [HttpGet("requester/{id}")]
    [Authorize(Roles = "Manager,BranchAdmin,TenantAdmin")]
    public async Task<IActionResult> GetByRequesterId(string id)
    {
        var result = await sender.Send(new GetPermitUpdateRequestByRequesterIdQuery(id));

        return NewResponse(result);
    }

    [HttpGet("latest")]
    [Authorize(Roles = "Manager,BranchAdmin,TenantAdmin")]
    public async Task<IActionResult> GetLatest()
    {
        var result = await sender.Send(new GetLatestPermitUpdateRequests());

        return NewResponse(result);
    }

    [HttpPost]
    [Authorize(Roles = "Requester")]
    public async Task<IActionResult> Create(CreatePermitUpdateRequestCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPut]
    [Authorize(Roles = "Requester")]
    public async Task<IActionResult> Update(UpdatePermitUpdateRequestCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }


}