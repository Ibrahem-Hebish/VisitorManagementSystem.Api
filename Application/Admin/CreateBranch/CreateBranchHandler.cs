using Application.Services.UnitOfWork;
using Domain.Branches;
using Domain.Branches.ObjectValues;
using Domain.Branches.Repositories;
using Domain.Tenants.ObjectValues;
using Domain.Tenants.Repositories;

namespace Application.Admin.CreateBranch;

public class CreateBranchHandler(
    IBranchCommandRepository branchCommandRepository,
    ITenantQueryRepository tenantQueryRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork


    )
    : ResponseHandler,
    IRequestHandler<CreateBranchCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var tenantId = httpContextAccessor.HttpContext.Request.Headers
                ["TenantId"].ToString();
            if (string.IsNullOrEmpty(tenantId))
                return BadRequest<string>("Tenant ID is not provided in the request headers.");

            var tenant = await tenantQueryRepository.GetByIdAsync(new TenantId(Guid.Parse(tenantId)), cancellationToken);

            if (tenant is null)
                return NotFouned<string>("Tenant not found.");

            var branchAddress = new BranchAddress(request.Country, request.City, request.Street);

            var branch = Branch.Create(request.BranchName, branchAddress, request.PhoneNumber, request.Email);

            branch.SetTenant(tenant);

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
