using Domain.Branches.Repositories;
using Domain.Tenants.ObjectValues;
using Domain.Users.Repositories.Employees;

namespace Application.Admin.CreateTenantAdmin;

public class CreateBranchAdminCommandHandler(
    IEmployeeCommandRepository employeeCommandRepository,
    IUserQueryRepository userQueryRepository,
    ITenantQueryRepository tenantQueryRepository,
    IPasswordHashingService passwordHashingService,
    IBranchQueryRepository BranchQueryRepository,
    ITenantService tenantService,
    IRoleQueryRepository roleQueryRepository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    )
    : ResponseHandler,
    IRequestHandler<CreateBranchAdminCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateBranchAdminCommand request, CancellationToken cancellationToken)
    {
        var tenantId = httpContextAccessor.HttpContext.Request.Headers
                ["TenantId"].ToString();

        if (string.IsNullOrEmpty(tenantId))
            return BadRequest<string>("Tenant ID is not provided in the request headers.");

        var branchId = httpContextAccessor.HttpContext.Request.Headers
                ["BranchId"].ToString();

        if (string.IsNullOrEmpty(tenantId))
            return BadRequest<string>("Branch ID is not provided in the request headers.");

        tenantService.SetBranchId(branchId);

        var tenant = await tenantQueryRepository.GetByIdAsync(new TenantId(Guid.Parse(tenantId)), cancellationToken);

        if (tenant is null)
            return NotFouned<string>("Tenant not found.");

        var branch = await BranchQueryRepository.GetByIdAsync(new BranchId(new Guid(branchId)), cancellationToken);

        if (branch is null)
            return NotFouned<string>("Branch is not found.");

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Password);

        var tenantAdmin = new Employee(request.FirstName, request.LastName, request.Email, hashedPassword, request.PhoneNumber);

        var role = await roleQueryRepository.GetRoleByName("BranchAdmin");

        if (role is null)
            return InternalServerError<string>();

        tenantAdmin.SetRole(role);

        await employeeCommandRepository.AddAsync(tenantAdmin);

        tenantAdmin.RaiseUserCreatedDomainEvent(branch.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Success<string>("Creating Tenant Admin done.");

    }
}
