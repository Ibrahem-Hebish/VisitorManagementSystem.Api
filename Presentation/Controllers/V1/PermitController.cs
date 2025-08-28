namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class PermitController(ISender sender) : AppControllerBase
{

    [HttpGet("{id}")]
    [Authorize(Roles = "BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetPermitByIdQuery(id));

        return NewResponse(result);
    }

    [HttpGet("{id}/details")]
    [Authorize(Roles = "BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetDetails(string id)
    {
        var result = await sender.Send(new GetPermitDetails(id));

        return NewResponse(result);
    }

    [HttpGet]
    [Authorize(Roles = "BranchAdmin,Manager,Security")]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllPermitsQuery());

        return NewResponse(result);
    }

    [HttpGet("paginate")]
    [Authorize(Roles = "BranchAdmin,Manager,Security")]
    public async Task<IActionResult> Paginate(PaginatePermits query)
    {
        var result = await sender.Send(query);

        return NewResponse(result);
    }

    [HttpGet("requester/{requesterId}")]
    [Authorize(Roles = "Requester")]
    public async Task<IActionResult> GetByRequesterId(string requesterId)
    {
        var result = await sender.Send(new GetPermitsCreatedByRequesterQuery(requesterId));

        return NewResponse(result);
    }

    [HttpGet("manager/{managerId}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetByManagerId(string managerId)
    {
        var result = await sender.Send(new GetPermitsHandledByManagerQuery(managerId));

        return NewResponse(result);
    }

    [HttpGet("visitor/{visitorId}")]
    [Authorize(Roles = "BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetByVisitorId(string visitorId)
    {
        var result = await sender.Send(new GetVistorPermitsQuery(visitorId));

        return NewResponse(result);
    }

    [HttpGet("latest")]
    [Authorize(Roles = "BranchAdmin,Manager,Security")]
    public async Task<IActionResult> GetLatest(int count)
    {
        var result = await sender.Send(new GetLatestPermitsQuery(count));

        return NewResponse(result);
    }

    [HttpGet("searchbydate")]
    [Authorize(Roles = "BranchAdmin,Manager,Security")]
    public async Task<IActionResult> SearchPermitByDate(DateTime startDate, DateTime endDate)
    {
        var result = await sender.Send(new SearchPermitByDateQuery(startDate, endDate));

        return NewResponse(result);
    }

    [HttpGet("{id}/entrylog")]
    [Authorize(Roles = "BranchAdmin,Manager,Requester,Security")]
    public async Task<IActionResult> GetPermitEntryLog(string id)
    {
        var result = await sender.Send(new GetPermitEntryLogQuery(id));

        return NewResponse(result);
    }

    [HttpGet("{id}/history")]
    [Authorize(Roles = "BranchAdmin,Manager,Requester,Security")]
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

    [HttpPatch("{id}/approve")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Approve(string id)
    {
        var result = await sender.Send(new ApprovePermitCommand(id));

        return NewResponse(result);
    }

    [HttpPatch("{id}/reject")]
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