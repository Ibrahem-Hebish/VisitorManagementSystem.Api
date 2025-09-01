using Domain.CatalogDb.Branches;
using Domain.CatalogDb.Branches.ObjectValues;
using Domain.CatalogDb.Tenants.ObjectValues;
using Domain.TenantDomain.Branches.DomainEvents;

namespace Application.Notification.Branchs;

public class NewBranchCreaedEventHandler(
    ISharedBranchCommandRepository sharedBranchCommandRepository,
    ISharedTenantQueryRepository sharedTenantQueryRepository)

    : INotificationHandler<NewBranchCreatedDomainEvent>
{
    public async Task Handle(NewBranchCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var address = new SharedBranchAddress(
                                   notification.BranchAddress.Country,
                                   notification.BranchAddress.City,
                                   notification.BranchAddress.Street
                                                    );

        var branchId = new SharedBranchId(notification.BranchId);

        var sharedBranch = SharedBranch.Create(
                                  branchId,
                                  notification.BranchName,
                                  address,
                                  notification.PhoneNumber,
                                  notification.Email);

        var tenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(notification.TenantId));

        if (tenant is null)
            throw new ArgumentNullException(nameof(tenant), "Tenant not found.");

        sharedBranch.SetTenant(tenant.Id);

        await sharedBranchCommandRepository.AddAsync(sharedBranch, cancellationToken);

    }
}
