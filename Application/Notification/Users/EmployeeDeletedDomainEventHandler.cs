using Domain.SharedTenantMetadataEntities.SharedUsers.ObjectValues;
using Domain.Users.DomainEvents;

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
            // logging will be here        
        }
    }
}
