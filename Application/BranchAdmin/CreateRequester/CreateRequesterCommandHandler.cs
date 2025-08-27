using Application.Services.UnitOfWork;
using Domain.Users.Repositories.Requesters;

namespace Application.BranchAdmin.CreateRequester;

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
        var branchId = httpContextAccessor.HttpContext.User.FindFirst("BranchId")?.Value;

        var managerId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Password);

        var requester = new Requester(request.FirstName, request.LastName, request.Email, hashedPassword, request.PhoneNumber);

        var role = await roleQueryRepository.GetRoleByName("Requester");

        if (role is null)
            return InternalServerError<string>();

        requester.SetRole(role);

        requester.SetManager(new UserId(new Guid(managerId)));

        await requesterCommandRepository.AddAsync(requester);

        requester.RaiseUserCreatedDomainEvent(new BranchId(new Guid(branchId!)));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Success("Creating Requester done.");

    }
}

