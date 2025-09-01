using Application.Features.Employees.CreateManager;
using Application.Features.Employees.CreateRequester;
using Application.Features.Employees.CreateSecurity;
using Domain.TenantDomain.Users.Enums;

namespace Application.Features.Employees.CreateEmployee;

public sealed class CreateEmployeeCommandHandler(
    ISender sender)

    : ResponseHandler,
    IRequestHandler<CreateEmployeeCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        return request.Position switch
        {
            EmployeePosition.BranchAdmin => UnAuthorize<string>(),

            EmployeePosition.BranchManager => await sender.Send(new CreateManagerCommand(request), cancellationToken),

            EmployeePosition.Requester => await sender.Send(new CreateRequesterCommand(request), cancellationToken),

            EmployeePosition.Security => await sender.Send(new CreateSecurityCommand(request), cancellationToken),

            _ => BadRequest<string>("Unkown position."),
        };
    }
}
