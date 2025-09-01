using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Roles.Repositories;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.Users.Repositories.Managers;
using Domain.TenantDomain.Users.Repositories.Users;

namespace Application.Features.Employees.CreateManager;

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
        var branchId = httpContextAccessor.HttpContext?.User.FindFirstValue("BranchId");

        var managerId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Command.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Command.Password);

        var manager = new Manager(request.Command.FirstName, request.Command.LastName, request.Command.Email,
                                    hashedPassword, request.Command.PhoneNumber, request.Command.Gender);

        var role = await roleQueryRepository.GetRoleByName("Manager");

        if (role is null)
            return InternalServerError<string>();

        manager.SetRole(role);

        manager.SetManager(new UserId(new Guid(managerId!)));

        await managerCommandRepository.AddAsync(manager);

        manager.RaiseUserCreatedDomainEvent(new BranchId(new Guid(branchId!)));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Created<string>("Creating Manager done.");

    }
}

