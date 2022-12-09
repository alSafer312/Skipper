using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Skipper.Models.DTOs.Incomig;
using Skipper.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Skipper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticateRequest model)
        {
            var response = await _userService.Register(model);

            if(response == null)
            {
                return BadRequest("Didn't register!");
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
            {
                return BadRequest("Email or password is incorrect");
            }

            return Ok(response);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var response = await _userService.Verify(token);

            if(response == null)
            {
                return BadRequest("Didn't verified");
            }

            return Ok(response);
        }

        [HttpPost("user-settings")]
        public async Task<IActionResult> UserSettings(UserSettingsRequest model)
        {
            _userService.SetUpSettings(model);
            return Ok(model);
        }

        [Authorize(Roles = "Menty")]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Hello menty");
        }

    }
}
