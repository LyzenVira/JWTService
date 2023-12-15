

using System.ComponentModel.DataAnnotations;

namespace JWTService.BLL.Models
{
    public class AddRoleModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
    }

}
