namespace Domain.Tokens.DomainEvents;

public record NewTokenUpdated(Guid Id, string AccessToken, string RefreshToken, DateTime AccessTokenExpirationDate, DateTime RefreshTOkenExpirationDate, string? BranchId = null)

  : DomainEvent
{
}


