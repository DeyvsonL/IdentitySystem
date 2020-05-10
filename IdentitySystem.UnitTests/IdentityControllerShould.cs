using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;
using IdentitySystem.API.Controllers;
using IdentitySystem.API.DAL;
using IdentitySystem.API.DTOs;
using IdentitySystem.API.Helpers;
using IdentitySystem.API.Models;
using IdentitySystem.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace IdentitySystem.UnitTests
{
    public class IdentityControllerShould
    {
        private readonly IdentityController controller;
        public IdentityControllerShould()
        {
            var options = new DbContextOptionsBuilder<IdentitySystemDbContext>()
                .UseInMemoryDatabase(databaseName: "IdentitySystemDatabase")
                .Options;
            var tokenService =
                new TokenService(Options.Create(new AppSettings{Secret = "Default secret for testing purpose should be greater than 1024"}));
            var userService = new UserService(new IdentitySystemDbContext(options));
            controller = new IdentityController(tokenService, userService);
        }
        
        [Fact]
        public void AuthenticateNotExisting_UserShouldReturnInvalidEmailOrPassword()
        {
            var userSignInDto = new UserSignInDto
            {
                Email = "notvalid@email.com",
                Password = "123456"
            };
            var actionResult = controller.Authenticate(userSignInDto).Result;
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal("Invalid e-mail or password", badRequestResult.Value);
        }
        
        [Fact]
        public void SignUpUser_ShouldReturnUserAndToken()
        {
            var userRegisterDto = BuildValidUserRegisterDto();
            var actionResult = controller.SignUp(userRegisterDto).Result;
            var createdRequestResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.IsType<RegisteredUserDto>(createdRequestResult.Value);
        }
        
        [Fact]
        public void UserRegisterDtoWithEmptyEmail_ShouldBeInvalid()
        {
            var userRegisterDto = BuildValidUserRegisterDto();
            userRegisterDto.Email = "";
            
            var context = new ValidationContext(userRegisterDto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(userRegisterDto, context, results, true);
            Assert.False(isValid);
        }

        [Fact]
        public void MeWithoutHeader_ShouldReturnBadRequest()
        {
            var meResult = controller.Me().Result;
            Assert.IsType<BadRequestObjectResult>(meResult);
        }

        private static UserRegisterDto BuildValidUserRegisterDto()
        {
            var userRegisterDto = new UserRegisterDto
            {
                Email = "notvalid@email.com",
                Password = "123456",
                FirstName = "Jose",
                LastName = "Maicon",
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        AreaCode = 81,
                        CountryCode = "+55",
                        Number = 56454654654
                    }
                }
            };
            return userRegisterDto;
        }
    }
}
