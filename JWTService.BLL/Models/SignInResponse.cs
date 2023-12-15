namespace JWTService.BLL.Models
{
    public class SignInResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }

    }
}
