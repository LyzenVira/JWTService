using JWTService.BLL.Models;
using JWTService.DAL.Entities;

namespace JWTService.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseEntity<SignInResponse>> SignInAsync(SignInModel model);
        Task<ResponseEntity<SignInResponse>> SignUpAsync(SignUpModel model);
        Task<ResponseEntity> ChangePassword(SignInModel model);
        bool RevokeToken(string token);
        ResponseEntity<Customer> GetById(Guid id);
        Task<ResponseEntity<string>> AddRoleAsync(AddRoleModel model);
    }
}
