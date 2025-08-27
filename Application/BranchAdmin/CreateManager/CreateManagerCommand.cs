namespace Application.BranchAdmin.CreateManager;
public record CreateManagerCommand : CreateBranchAdminCommand, IRequest<Response<string>>, IValidatorRequest
{
}

