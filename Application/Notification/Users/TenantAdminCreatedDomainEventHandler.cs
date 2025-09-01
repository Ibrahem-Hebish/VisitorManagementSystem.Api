using Domain.CatalogDb.Repositories;
using Domain.CatalogDb.SharedUsers.ObjectValues;
using Domain.CatalogDb.Tenants.ObjectValues;
using Domain.TenantDomain.Users.DomainEvents;

namespace Application.Notification.Users;

public class TenantAdminCreatedDomainEventHandler(
    ISharedTenantCommandRepository sharedTenantCommandRepository,
    ISharedTenantQueryRepository sharedTenantQueryRepository) : INotificationHandler<TenantAdminCreatedDomainEvent>
{
    public async Task Handle(TenantAdminCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var sharedTenant = await sharedTenantQueryRepository.GetByIdAsync(
                                            new SharedTenantId(new Guid(notification.Tenant.Id.Value.ToString())),
                                            cancellationToken
);

        if (sharedTenant is not null)
        {
            sharedTenant.SetManager(new SharedUserId(new Guid(notification.UserId)));
            await sharedTenantCommandRepository.UpdateAsync(sharedTenant, cancellationToken);
        }
    }
}
