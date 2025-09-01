

using Domain.TenantDomain.Users.Enums;

namespace Presentation.Controllers.V1;

[ApiVersion(1.0)]
public class EmployeesController(ISender sender) : AppControllerBase
{

    [HttpGet]
    [Authorize(Roles = "TenantAdmin,BranchAdmin")]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetAllEmployeesQuery());

        return NewResponse(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await sender.Send(new GetEmployeeByIdQuery(id));

        return NewResponse(result);
    }

    [HttpGet("managers")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin")]
    public async Task<IActionResult> GetAllManagers()
    {
        var result = await sender.Send(new GetAllManagersQuery());

        return NewResponse(result);
    }

    [HttpGet("requesters")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager")]
    public async Task<IActionResult> GetAllRequestes()
    {
        var result = await sender.Send(new GetAllRequesterQuery());

        return NewResponse(result);
    }

    [HttpGet("securities")]
    [Authorize(Roles = "TenantAdmin,BranchAdmin,Manager")]
    public async Task<IActionResult> GetAllSecurities()
    {
        var result = await sender.Send(new GetAllSecuritiesQuery());

        return NewResponse(result);
    }

    [HttpPost]
    [Authorize(Roles = "BranchAdmin")]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        var result = await sender.Send(command);

        return NewResponse(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "BranchAdmin")]
    public async Task<IActionResult> Delete(string id, EmployeePosition position)
    {
        var result = await sender.Send(new DeleteEmployeeCommand(id, position));

        return NewResponse(result);
    }

    [HttpPost("admin")]
    [Authorize(Roles = "TenantAdmin")]
    public async Task<IActionResult> CreateBranchAdmin(CreateBranchAdminCommand command)
    {

        var response = await sender.Send(command);

        return NewResponse(response);
    }

    [HttpDelete("admin/{id}")]
    [Authorize(Roles = "TenantAdmin")]
    public async Task<IActionResult> DeleteBranchAdmin(string id)
    {
        var result = await sender.Send(new DeleteBranchAdminCommand(id));

        return NewResponse(result);
    }

}
