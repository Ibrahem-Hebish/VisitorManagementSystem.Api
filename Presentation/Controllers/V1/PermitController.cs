namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class PermitController(ISender sender) : AppControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetPermitByIdQuery(id));

        return NewResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllPermitsQuery());

        return NewResponse(result);
    }

    [HttpGet("Requester/{id}")]
    public async Task<IActionResult> GetByRequesterId(string id)
    {
        var result = await sender.Send(new GetPermitsCreatedByRequesterQuery(id));

        return NewResponse(result);
    }

    [HttpGet("Manager/{id}")]
    public async Task<IActionResult> GetByManagerId(string id)
    {
        var result = await sender.Send(new GetPermitsHandledByManagerQuery(id));

        return NewResponse(result);
    }



}