using Application.Features.Employees.DeleteManager;
using Application.Features.Employees.DeleteRequester;
using Application.Features.Employees.DeleteSecurity;
using Domain.TenantDomain.Users.Enums;

namespace Application.Features.Employees.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler(
    ISender sender)

    : ResponseHandler,
    IRequestHandler<DeleteEmployeeCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        return request.Position switch
        {
            EmployeePosition.BranchAdmin => UnAuthorize<string>(),

            EmployeePosition.BranchManager => await sender.Send(new DeleteManagerCommand(request.Id), cancellationToken),

            EmployeePosition.Requester => await sender.Send(new DeleteRequesterCommand(request.Id), cancellationToken),

            EmployeePosition.Security => await sender.Send(new DeleteSecurityCommand(request.Id), cancellationToken),

            _ => BadRequest<string>("Unkown position."),
        };
    }
}
