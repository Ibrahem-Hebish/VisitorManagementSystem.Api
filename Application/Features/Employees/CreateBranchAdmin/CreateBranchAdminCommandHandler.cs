using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Branches.Repositories;
using Domain.TenantDomain.Roles.Enums;
using Domain.TenantDomain.Roles.Repositories;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.Repositories.Employees;
using Domain.TenantDomain.Users.Repositories.Users;

namespace Application.Features.Admin.CreateTenantAdmin;

public class CreateBranchAdminCommandHandler(
    IEmployeeCommandRepository employeeCommandRepository,
    IUserQueryRepository userQueryRepository,
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

        var branchId = httpContextAccessor.HttpContext?.Request.Headers
                ["BranchId"].ToString();

        if (string.IsNullOrEmpty(branchId))
            return BadRequest<string>("Branch ID is not provided in the request headers.");

        tenantService.SetBranchId(branchId);

        var branch = await BranchQueryRepository.GetByIdAsync(new BranchId(new Guid(branchId)), cancellationToken);

        if (branch is null)
            return NotFound<string>("Branch is not found.");

        var exsistedUser = await userQueryRepository.GetByEmailAsync(request.Email);

        if (exsistedUser is not null)
            return BadRequest<string>("User has already exsisted.");

        var hashedPassword = passwordHashingService.HashPasswordBCrypt(request.Password);

        var branchAdmin = new Employee(request.FirstName, request.LastName, request.Email,
                                         hashedPassword, request.PhoneNumber, request.Gender);

        var role = await roleQueryRepository.GetRoleByName(Roles.BranchAdmin.ToString());

        if (role is null)
            return InternalServerError<string>();

        branchAdmin.SetRole(role);

        await employeeCommandRepository.AddAsync(branchAdmin);

        branchAdmin.RaiseUserCreatedDomainEvent(branch.Id);

        branch.SetManager(branchAdmin.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Created<string>("Creating Branch Admin done.");

    }
}
