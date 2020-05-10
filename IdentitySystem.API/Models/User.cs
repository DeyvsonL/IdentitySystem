using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using IdentitySystem.API.DTOs;

namespace IdentitySystem.API.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        public List<Phone> Phones { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }

        public User() {}
        public User(UserRegisterDto userRegisterDto)
        {
            FirstName = userRegisterDto.FirstName;
            LastName = userRegisterDto.LastName;
            Email = userRegisterDto.Email;
            Password = userRegisterDto.Password;
            Phones = userRegisterDto.Phones;
        }

    }
}
