using IdentitySystem.API.Helpers;
using IdentitySystem.API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentitySystem.API.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EnsureOneElement(ErrorMessage = "At least a phone is required")]
        public List<Phone> Phones { get; set; }

    }
}