namespace Application.BranchAdmin.CreateSecurity;
public record CreateSecurityCommand : CreateBranchAdminCommand, IRequest<Response<string>>, IValidatorRequest
{
}

