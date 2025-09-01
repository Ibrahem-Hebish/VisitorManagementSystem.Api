using Domain.TenantDomain.Common;
using Domain.TenantDomain.Tokens.DomainEvents;
using Domain.TenantDomain.Tokens.ValueObjects;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Tokens;

public class UserToken : Entity
{
    public UserTokenId Id { get; private set; }
    public string AccessToken { get; private set; }
    public DateTime AccessTokenExpiredDate { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiredDate { get; private set; }
    public bool InUse { get; private set; }
    public UserId UserId { get; private set; }
    public User User { get; private set; }

    private UserToken() { }
    private UserToken(string accessToken, DateTime accessTokenExpiredDate, string refreshToken, DateTime refreshTokenExpiredDate, UserId userId)
    {
        Id = new UserTokenId(Guid.NewGuid());
        AccessToken = accessToken;
        AccessTokenExpiredDate = accessTokenExpiredDate;
        RefreshToken = refreshToken;
        RefreshTokenExpiredDate = refreshTokenExpiredDate;
        UserId = userId;
        InUse = true;

    }

    private UserToken(UserTokenId id, string accessToken, DateTime accessTokenExpiredDate, string refreshToken, DateTime refreshTokenExpiredDate, UserId userId)
    {
        Id = id;
        AccessToken = accessToken;
        AccessTokenExpiredDate = accessTokenExpiredDate;
        RefreshToken = refreshToken;
        RefreshTokenExpiredDate = refreshTokenExpiredDate;
        UserId = userId;
        InUse = true;

    }

    public static UserToken Create(string accessToken, DateTime accessTokenExpiredDate, string refreshToken, DateTime refreshTokenExpiredDate, UserId userId)
    {
        return new(accessToken, accessTokenExpiredDate, refreshToken, refreshTokenExpiredDate, userId);
    }

    public static UserToken Create(UserTokenId id, string accessToken, DateTime accessTokenExpiredDate, string refreshToken, DateTime refreshTokenExpiredDate, UserId userId)
    {
        var token = new UserToken(id, accessToken, accessTokenExpiredDate, refreshToken, refreshTokenExpiredDate, userId);

        return token;
    }

    public void Update(string accessToken, string refreshToken, DateTime refreshTokenExpiredDate, DateTime accessTokenExpiredDate)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        RefreshTokenExpiredDate = refreshTokenExpiredDate;
        AccessTokenExpiredDate = accessTokenExpiredDate;
        InUse = true;
    }

    public void MarkAsInactive()
    {
        InUse = false;
        RefreshTokenExpiredDate = DateTime.UtcNow;
        AccessTokenExpiredDate = DateTime.UtcNow;
    }

    public void RaiseNewTokenCreatedEvent(string? branchId)
    {
        Raise(new NewTokenCreated(Id.Value, AccessToken, RefreshToken, AccessTokenExpiredDate, RefreshTokenExpiredDate, branchId));
    }

    public void RaiseTokenUpdatedEvent(string? branchId)
    {
        Raise(new NewTokenUpdated(Id.Value, AccessToken, RefreshToken, AccessTokenExpiredDate, RefreshTokenExpiredDate, branchId));
    }
}
