namespace JWTService.BLL.Models;

public class JwtOptions
{
    public string SecretKey { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public int TokenLifeTime { get; set; }
}
