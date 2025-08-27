using Domain.Tokens.DomainEvents;

namespace Application.Notification.UserTokens;

public class TokenUpdatedDomainEventHandler(
       ISharedUserTokenCommandRepository sharedUserTokenCommandRepository,
       ISharedUserTokenQueryRepository sharedUserTokenQueryRepository) : INotificationHandler<NewTokenUpdated>
{
    public async Task Handle(NewTokenUpdated notification, CancellationToken cancellationToken)
    {
        var token = await sharedUserTokenQueryRepository.GetByIdAsync(
                                   new SharedUserTokenId(notification.Id), cancellationToken);

        if (token is null)
            return;

        token.Update(notification.AccessToken, notification.RefreshToken, notification.RefreshTOkenExpirationDate, notification.AccessTokenExpirationDate);

        await sharedUserTokenCommandRepository.UpdateAsync(token);
    }
}
