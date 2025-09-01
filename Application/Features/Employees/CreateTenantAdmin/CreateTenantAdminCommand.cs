namespace Application.Features.Admin.CreateTenantAdmin;

public record CreateTenantAdminCommand : CreateBranchAdminCommand, IRequest<Response<string>>, IValidatorRequest
{

}

