using IdentitySystem.API.Models;

namespace IdentitySystem.API.DTOs
{
    public class RegisteredUserDto
    {
        public User User { get; set; }
        public string Token { get; set; }

        public RegisteredUserDto(User user, string token)
        {
            User = user;
            Token = token;
        }
    }
}