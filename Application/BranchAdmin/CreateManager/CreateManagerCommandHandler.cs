using Application.Services.UnitOfWork;
using Domain.Users.Repositories.Managers;

namespace Application.BranchAdmin.CreateManager;

public class CreateManagerCommandHandler(
    IManagerCommandRepository managerCommandRepository,
    IUserQueryRepository userQueryRepository,
    IPasswordHashingService passwordHashingService,
    IRoleQueryRepository roleQueryRepository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    )
    : ResponseHandler,
    IRequestHandler<CreateManagerCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        var branchId = httpContextAccessor.HttpContext.User.FindFirst("BranchId")?.Value;

        var managerId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Password);

        var manager = new Manager(request.FirstName, request.LastName, request.Email, hashedPassword, request.PhoneNumber);

        var role = await roleQueryRepository.GetRoleByName("Manager");

        if (role is null)
            return InternalServerError<string>();

        manager.SetRole(role);

        manager.SetManager(new UserId(new Guid(managerId)));

        await managerCommandRepository.AddAsync(manager);

        manager.RaiseUserCreatedDomainEvent(new BranchId(new Guid(branchId!)));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Success("Creating Manager done.");

    }
}

