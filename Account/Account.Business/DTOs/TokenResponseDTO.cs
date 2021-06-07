namespace Account.Business.DTOs
{
    public record TokenResponseDTO
    {
        public string AccessToken { get; init; }
        public int AccessTokenExpiresIn { get; init; }
        public string RefreshToken { get; init; }
        public int RefreshTokenExpiresIn { get; init; }
    }
}
