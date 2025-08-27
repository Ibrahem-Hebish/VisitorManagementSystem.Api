using Domain.SharedTenantMetadataEntities.Branches.ObjectValues;
using Domain.SharedTenantMetadataEntities.SharedUsers;
using Domain.SharedTenantMetadataEntities.SharedUsers.ObjectValues;
using Domain.Users.DomainEvents;

namespace Application.Notification.Users;

public class NewEmployeeCreatedDomainEventHandler(
    ISharedUserCommandRepository sharedUserCommandRepository)

    : INotificationHandler<NewEmployeeCreatedDomainEvent>
{
    public async Task Handle(NewEmployeeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var sharedUser = SharedUser.Create(
                                       notification.FirstName,
                                       notification.Email,
                                       notification.PhoneNumber
            );

        sharedUser.SetId(new SharedUserId(notification.Id));

        sharedUser.SetBranch(new SharedBranchId(notification.BranchId.Guid));

        await sharedUserCommandRepository.AddAsync(sharedUser, cancellationToken);
    }
}
