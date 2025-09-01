using Domain.CatalogDb.SharedUsers.ObjectValues;
using Domain.TenantDomain.Users.DomainEvents;
using Serilog;

namespace Application.Notification.Users;

public sealed class EmployeeDeletedDomainEventHandler(
    ISharedUserQueryRepository sharedUserQueryRepository,
    ISharedUserCommandRepository sharedUserCommandRepository)

    : INotificationHandler<EmployeeDeletedDomainEvent>
{
    public async Task Handle(EmployeeDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var user = await sharedUserQueryRepository.GetByIdAsync(new SharedUserId(new Guid(notification.UserId)), cancellationToken);

            if (user is not null)
                await sharedUserCommandRepository.DeleteAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            Log.Error($"failed to add employee to shared db. message: {ex.Message}");
        }
    }
}
