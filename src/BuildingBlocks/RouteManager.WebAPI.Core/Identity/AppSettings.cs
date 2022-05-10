namespace RouteManager.WebAPI.Core.Identity;

public class AppSettings
{
    public int ExpirationHours { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public int RefreshTokenExpiration { get; init; }
}