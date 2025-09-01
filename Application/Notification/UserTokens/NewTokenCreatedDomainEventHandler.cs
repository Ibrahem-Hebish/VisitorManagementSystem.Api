using Domain.CatalogDb.Branches.ObjectValues;
using Domain.TenantDomain.Tokens.DomainEvents;

namespace Application.Notification.UserTokens;

public class NewTokenCreatedDomainEventHandler(
    ISharedUserTokenCommandRepository sharedUserTokenCommandRepository) : INotificationHandler<NewTokenCreated>
{
    public async Task Handle(NewTokenCreated notification, CancellationToken cancellationToken)
    {
        var sharedUserToken = SharedUserToken.Create(
                       new SharedUserTokenId(notification.Id),
                       notification.AccessToken,
                       notification.AccessTokenExpirationDate,
                       notification.RefreshToken,
                       notification.RefreshTOkenExpirationDate,
                       true);

        if (notification.BranchId is not null)
            sharedUserToken.SetBranchId(new SharedBranchId(new Guid(notification.BranchId)));

        await sharedUserTokenCommandRepository.AddAsync(sharedUserToken);
    }
}
