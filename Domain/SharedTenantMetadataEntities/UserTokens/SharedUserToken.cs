using Domain.SharedTenantMetadataEntities.Branches;
using Domain.SharedTenantMetadataEntities.UserTokens.ObjectValues;

namespace Domain.SharedTenantMetadataEntities.UserTokens;

public class SharedUserToken
{
    public SharedUserTokenId Id { get; private set; }
    public string AccessToken { get; private set; }
    public DateTime AccessTokenExpiredDate { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiredDate { get; private set; }
    public bool InUse { get; private set; }
    public SharedBranchId? BranchId { get; private set; }
    public SharedBranch? SharedBranch { get; private set; }

    private SharedUserToken() { }

    private SharedUserToken(SharedUserTokenId id, string accessToken, DateTime accessTokenExpiredDate, string refreshToken, DateTime refreshTokenExpiredDate, bool inUse)
    {
        this.Id = id;
        AccessToken = accessToken;
        AccessTokenExpiredDate = accessTokenExpiredDate;
        RefreshToken = refreshToken;
        RefreshTokenExpiredDate = refreshTokenExpiredDate;
        InUse = inUse;
    }

    public static SharedUserToken Create(SharedUserTokenId id, string accessToken, DateTime accessTokenExpiredDate, string refreshToken, DateTime refreshTokenExpiredDate, bool inUse)
                 => new(id, accessToken, accessTokenExpiredDate, refreshToken, refreshTokenExpiredDate, inUse);

    public void SetBranch(SharedBranch branch)
    {
        SharedBranch = branch;
        BranchId = branch.Id;
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
        AccessTokenExpiredDate = DateTime.UtcNow;
        RefreshTokenExpiredDate = DateTime.UtcNow;
    }

    public void SetBranchId(SharedBranchId sharedBranchId)
    {
        BranchId = sharedBranchId;
    }
}






