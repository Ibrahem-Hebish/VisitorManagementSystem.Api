using Domain.TenantDomain.Common;

namespace Domain.TenantDomain.Tokens.DomainEvents;

public record NewTokenCreated(Guid Id, string AccessToken, string RefreshToken,
    DateTime AccessTokenExpirationDate, DateTime RefreshTOkenExpirationDate, string BranchId = null)

    : DomainEvent
{

}


