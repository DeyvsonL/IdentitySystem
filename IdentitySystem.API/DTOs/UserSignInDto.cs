using System.ComponentModel.DataAnnotations;

namespace IdentitySystem.API.DTOs
{
    public class UserSignInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } 
    }
}
