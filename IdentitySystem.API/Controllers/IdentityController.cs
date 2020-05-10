using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentitySystem.API.DTOs;
using IdentitySystem.API.Models;
using IdentitySystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySystem.API.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly UserService _userService;

        public IdentityController(TokenService tokenService, UserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(UserSignInDto userDto)
        {
            try
            {
                var user = await _userService.SignIn(userDto.Email, userDto.Password);

                var token = _tokenService.GenerateToken(user);
                return Ok(new
                {
                    user = (
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.Phones,
                        user.CreatedAt,
                        user.LastLogin
                    ),
                    token
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            try
            {
                var user = await _userService.Get(long.Parse(User.Claims.First(claim => claim.Type.Equals(ClaimTypes.Name)).Value));
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserRegisterDto userRegisterDto)
        {
            try
            {
                var user = new User(userRegisterDto);
                await _userService.SignUp(user);
                var token = _tokenService.GenerateToken(user);
                return Created("me", new RegisteredUserDto(user,token));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
