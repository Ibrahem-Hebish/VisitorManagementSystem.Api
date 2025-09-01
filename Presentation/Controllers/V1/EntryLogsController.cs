namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
[Authorize(Roles = "Security")]
public class EntryLogsController(ISender sender) : AppControllerBase
{
    [HttpPost("{permitId}")]
    public async Task<IActionResult> Create(string permitId)
    {
        var result = await sender.Send(new CreateEntryLogCommand(permitId));

        return NewResponse(result);
    }

}