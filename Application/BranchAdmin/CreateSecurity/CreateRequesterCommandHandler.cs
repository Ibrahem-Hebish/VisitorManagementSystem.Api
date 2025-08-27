using Application.Services.UnitOfWork;

namespace Application.BranchAdmin.CreateSecurity;

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
        var branchId = httpContextAccessor.HttpContext.User.FindFirst("BranchId")?.Value;

        var managerId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Password);

        var security = new Security(request.FirstName, request.LastName, request.Email, hashedPassword, request.PhoneNumber);

        var role = await roleQueryRepository.GetRoleByName("Security");

        if (role is null)
            return InternalServerError<string>();

        security.SetRole(role);

        security.SetManager(new UserId(new Guid(managerId)));

        await securityCommandRepository.AddAsync(security);

        security.RaiseUserCreatedDomainEvent(new BranchId(new Guid(branchId!)));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Success("Creating Security done.");

    }
}

