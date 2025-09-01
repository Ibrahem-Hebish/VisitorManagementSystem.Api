namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class PermitsController(ISender sender) : AppControllerBase
{

    [HttpGet("{id}")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetPermitByIdQuery(id));

        return NewResponse(result);
    }

    [HttpGet("details/{id}")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetDetails(string id)
    {
        var result = await sender.Send(new GetPermitDetails(id));

        return NewResponse(result);
    }

    [HttpGet]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Security")]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllPermitsQuery());

        return NewResponse(result);
    }

    [HttpGet("paginate")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Security")]
    public async Task<IActionResult> Paginate(PaginatePermits query)
    {
        var result = await sender.Send(query);

        return NewResponse(result);
    }

    [HttpGet("requester/{requesterId}")]
    [Authorize(Roles = "TenantAdmin,Requester,Manager,BranchAdmin")]
    public async Task<IActionResult> GetByRequesterId(string requesterId)
    {
        var result = await sender.Send(new GetPermitsCreatedByRequesterQuery(requesterId));

        return NewResponse(result);
    }

    [HttpGet("manager/{managerId}")]
    [Authorize(Roles = "TenantAdmin,Manager,BranchAdmin")]
    public async Task<IActionResult> GetByManagerId(string managerId)
    {
        var result = await sender.Send(new GetPermitsHandledByManagerQuery(managerId));

        return NewResponse(result);
    }

    [HttpGet("latest")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Security")]
    public async Task<IActionResult> GetLatest(int count)
    {
        var result = await sender.Send(new GetLatestPermitsQuery(count));

        return NewResponse(result);
    }

    [HttpGet("searchbydate")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Security")]
    public async Task<IActionResult> SearchPermitByDate(DateTime startDate, DateTime endDate)
    {
        var result = await sender.Send(new SearchPermitByDateQuery(startDate, endDate));

        return NewResponse(result);
    }

    [HttpGet("entrylog/{id}")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetPermitEntryLog(string id)
    {
        var result = await sender.Send(new GetPermitEntryLogQuery(id));

        return NewResponse(result);
    }

    [HttpGet("history/{id}")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetPermitHistory(string id)
    {
        var result = await sender.Send(new GetPermitHistoryQuery(id));

        return NewResponse(result);
    }

    [HttpPost]
    [Authorize(Roles = "Requester")]
    public async Task<IActionResult> Create([FromForm] CreatePermitCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpPatch("approve/{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Approve(string id)
    {
        var result = await sender.Send(new ApprovePermitCommand(id));

        return NewResponse(result);
    }

    [HttpPatch("reject/{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Reject(string id)
    {
        var result = await sender.Send(new RejectPermitCommand(id));

        return NewResponse(result);
    }

    [HttpPatch("extend")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Extend(ExtendPermitCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await sender.Send(new DeletePermitCommand(id));

        return NewResponse(result);
    }
}
