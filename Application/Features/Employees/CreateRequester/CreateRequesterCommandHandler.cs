
namespace Application.Features.Employees.CreateRequester;

public class CreateRequesterCommandHandler(
    IRequesterCommandRepository requesterCommandRepository,
    IUserQueryRepository userQueryRepository,
    IPasswordHashingService passwordHashingService,
    IRoleQueryRepository roleQueryRepository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    )
    : ResponseHandler,
    IRequestHandler<CreateRequesterCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateRequesterCommand request, CancellationToken cancellationToken)
    {
        var branchId = httpContextAccessor.HttpContext?.User.FindFirstValue("BranchId");

        var managerId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Command.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Command.Password);

        var requester = new Requester(request.Command.FirstName, request.Command.LastName,
                                    request.Command.Email, hashedPassword, request.Command.PhoneNumber, request.Command.Gender);

        var role = await roleQueryRepository.GetRoleByName("Requester");

        if (role is null)
            return InternalServerError<string>();

        requester.SetRole(role);

        requester.SetManager(new UserId(new Guid(managerId!)));

        await requesterCommandRepository.AddAsync(requester);

        requester.RaiseUserCreatedDomainEvent(new BranchId(new Guid(branchId!)));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Created<string>("Creating Requester done.");

    }
}

