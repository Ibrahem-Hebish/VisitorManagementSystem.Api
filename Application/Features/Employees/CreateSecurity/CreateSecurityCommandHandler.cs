using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Roles.Repositories;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.Users.Repositories.Securities;
using Domain.TenantDomain.Users.Repositories.Users;

namespace Application.Features.Employees.CreateSecurity;

public class CreateSecurityCommandHandler(
    ISecurityCommandRepository securityCommandRepository,
    IUserQueryRepository userQueryRepository,
    IPasswordHashingService passwordHashingService,
    IRoleQueryRepository roleQueryRepository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    )
    : ResponseHandler,
    IRequestHandler<CreateSecurityCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateSecurityCommand request, CancellationToken cancellationToken)
    {
        var branchId = httpContextAccessor.HttpContext?.User.FindFirstValue("BranchId");

        var managerId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Command.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Command.Password);

        var security = new Security(request.Command.FirstName, request.Command.LastName,
                                    request.Command.Email, hashedPassword, request.Command.PhoneNumber, request.Command.Gender);

        var role = await roleQueryRepository.GetRoleByName("Security");

        if (role is null)
            return InternalServerError<string>();

        security.SetRole(role);

        security.SetManager(new UserId(new Guid(managerId!)));

        await securityCommandRepository.AddAsync(security);

        security.RaiseUserCreatedDomainEvent(new BranchId(new Guid(branchId!)));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Created<string>("Creating Security done.");

    }
}

