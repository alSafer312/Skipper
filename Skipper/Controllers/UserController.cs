using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skipper.Models.DTOs.Incomig;
using Skipper.Services;

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
                return BadRequest(response);
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

        [Authorize]
        [HttpPost("user-settings")]
        public async Task<IActionResult> UserSettings(UserSettingsRequest model)
        {
            var response = await _userService.SetUpSettings(model);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar(/*[FromForm]*/ IFormFile file)
        {
            return Ok(await _userService.UploadAvatar(file));
        }

        [Authorize]
        [HttpGet("user-settings")]
        public IActionResult GetUserSettings()
        {
            var response = _userService.GetUpSettings();
            if(response == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> UserDelete()
        {
            if (!await _userService.UserDelete())
            {
                return BadRequest("Can't delete");
            }
            return Ok("User Deleted");
        }

        [Authorize]
        [HttpGet("communication-types")]
        public IActionResult GetCommynicationTypes()
        {
            return Ok(_userService.GetCommynicationTypes());
        }

        [Authorize]
        [HttpGet("time-zones")]
        public IActionResult GetTimeZones()
        {
            return Ok(_userService.GetTimeZones());
        }
    }
}
