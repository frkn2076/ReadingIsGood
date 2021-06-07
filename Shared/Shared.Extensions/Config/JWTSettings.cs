namespace Shared.Extensions.Config
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string Scheme { get; set; }
        public int AccessTokenExpiresIn { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
    }
}
