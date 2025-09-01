using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Roles.Enums;
using Domain.TenantDomain.Roles.Repositories;
using Domain.TenantDomain.Tenants.ObjectValues;
using Domain.TenantDomain.Tenants.Repositories;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.Users.Repositories.Employees;
using Domain.TenantDomain.Users.Repositories.Users;

namespace Application.Features.Admin.CreateTenantAdmin;

public class CreateTenantAdminCommandHandler(
    IEmployeeCommandRepository employeeCommandRepository,
    IUserQueryRepository userQueryRepository,
    IPasswordHashingService passwordHashingService,
    ITenantQueryRepository tenantQueryRepository,
    IRoleQueryRepository roleQueryRepository,
    IUnitOfWork unitOfWork,
    ITenantService tenantService,
    IHttpContextAccessor httpContextAccessor
    )
    : ResponseHandler,
    IRequestHandler<CreateTenantAdminCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateTenantAdminCommand request, CancellationToken cancellationToken)
    {
        var tenantId = httpContextAccessor.HttpContext?.Request.Headers
                ["TenantId"].ToString();

        if (string.IsNullOrEmpty(tenantId))
            return BadRequest<string>("Tenant Id is not provided in the request headers.");

        var tenant = await tenantQueryRepository.GetByIdAsync(new TenantId(new Guid(tenantId)), cancellationToken);

        if (tenant is null)
            return NotFound<string>();

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Password);

        var tenantAdmin = new Employee(request.FirstName, request.LastName, request.Email,
                                          hashedPassword, request.PhoneNumber, request.Gender);

        var role = await roleQueryRepository.GetRoleByName(Roles.TenantAdmin.ToString());

        if (role is null)
            return InternalServerError<string>();

        tenantAdmin.SetRole(role);

        await employeeCommandRepository.AddAsync(tenantAdmin);

        var branchId = httpContextAccessor.HttpContext?.Request.Headers["BranchId"];

        tenantService.SetBranchId(branchId!);

        tenantAdmin.RaiseUserCreatedDomainEvent(new BranchId(new Guid(branchId!)));

        tenantAdmin.RaiseTenantAdminCreatedDomainEvent(tenantAdmin.Id.Value.ToString(), tenant);

        tenant.SetManager(new UserId(new Guid(tenantAdmin.Id.Value.ToString())));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Success("Creating Tenant Admin done.");

    }
}
