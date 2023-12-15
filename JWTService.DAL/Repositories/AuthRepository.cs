

using JWTService.DAL.Context;
using JWTService.DAL.Entities;
using JWTService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTService.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IdentityDataContext _context;
       
        public AuthRepository(IdentityDataContext identityDataContext)
        {
            _context = identityDataContext;
        }

        public bool RevokeToken(string token)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t
            => t.Token == token));
            if (user == null) return false;
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive) return false;

            refreshToken.Revoked = DateTime.UtcNow;
            _context.Update(user);
            _context.SaveChanges();
            return true;
        }
        public Customer GetById(Guid id)
        {
            return _context.Users.Find(id);
        }
    }
}
