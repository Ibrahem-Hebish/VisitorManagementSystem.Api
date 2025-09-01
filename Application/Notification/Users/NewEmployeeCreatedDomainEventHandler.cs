using Domain.CatalogDb.Branches.ObjectValues;
using Domain.CatalogDb.SharedUsers;
using Domain.CatalogDb.SharedUsers.ObjectValues;
using Domain.TenantDomain.Users.DomainEvents;

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

        sharedUser.SetBranch(new SharedBranchId(notification.BranchId.Value));

        await sharedUserCommandRepository.AddAsync(sharedUser, cancellationToken);
    }
}
