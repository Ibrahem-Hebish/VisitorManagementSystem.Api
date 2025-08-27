namespace Application.Dtos.UserToken;

public record UserTokenDto
{
    public DateTime AccessTokenExpiredDate { get; init; }
    public DateTime RefreshTokenExpiredDate { get; init; }
    public bool IsActive { get; init; }
    public string UserId { get; init; }
    public string Role { get; set; } = string.Empty;
}
