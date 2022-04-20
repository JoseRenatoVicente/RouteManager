namespace RouteManager.WebAPI.Core.Identity
{
    public class AppSettings
    {
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
