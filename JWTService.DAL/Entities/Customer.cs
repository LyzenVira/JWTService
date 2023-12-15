
using Microsoft.AspNetCore.Identity;

namespace JWTService.DAL.Entities
{
    public  class Customer : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<RefreshToken> RefreshTokens { get; set; } 

    }
}
