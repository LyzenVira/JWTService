

using JWTService.DAL.Entities;

namespace JWTService.DAL.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        bool RevokeToken(string token);
        Customer GetById(Guid id);
    }
}
