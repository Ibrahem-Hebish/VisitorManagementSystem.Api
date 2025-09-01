using Domain.CatalogDb.Tenants.ObjectValues;
using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Branches.Repositories;
using Domain.TenantDomain.Roles.Enums;
using Domain.TenantDomain.Tenants.ObjectValues;

namespace Application.Features.Brnches.CreateBranch;

public class CreateBranchHandler(
    IBranchCommandRepository branchCommandRepository,
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    IHttpContextAccessor httpContextAccessor,
    IConnectionStringProtector connectionStringProtector,
    ITenantService tenantService,
    IUnitOfWork unitOfWork


    )
    : ResponseHandler,
    IRequestHandler<CreateBranchCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var role = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            string tenantId = null;

            if (role == Roles.Admin.ToString())
            {
                tenantId = httpContextAccessor.HttpContext?.Request.Headers["TenantId"]!;
            }
            else
            {
                tenantId = httpContextAccessor.HttpContext?.User.FindFirstValue("TenantId")!;

            }

            var tenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(new Guid(tenantId!)), cancellationToken);

            if (tenant is null)
                return NotFound<string>();

            var connectionsString = connectionStringProtector.Decrypt(tenant.ConnectionString);

            tenantService.SetConnectionString(connectionsString);

            var branchAddress = new BranchAddress(request.Country, request.City, request.Street);

            var branch = Branch.Create(request.BranchName, branchAddress, request.PhoneNumber, request.Email);

            branch.SetTenant(new TenantId(new Guid(tenantId!)));

            branch.RaiseNewBranchDomainEvent();

            branchCommandRepository.AddAsync(branch, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Success("Branch created successfully.");
        }
        catch
        {
            return InternalServerError<string>("An error occurred while processing the request.");
        }

    }
}
