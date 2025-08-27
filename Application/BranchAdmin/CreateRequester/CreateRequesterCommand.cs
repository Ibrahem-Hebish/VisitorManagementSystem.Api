namespace Application.BranchAdmin.CreateRequester;
public record CreateRequesterCommand : CreateBranchAdminCommand, IRequest<Response<string>>, IValidatorRequest
{
}

