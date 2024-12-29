using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.Authentication.Commands;
using PulseDonor.Application.Authentication.Interfaces;

namespace PulseDonor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;

        public AuthController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupCommand command)
        {
            var result = await _userService.SignupAsync(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
			try
			{
				var result = await _userService.LoginAsync(command);
				return Ok(result); // Ensure it's wrapped in a valid JSON object
			}
			catch (UnauthorizedAccessException ex)
			{
				return Unauthorized(new { Error = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Error = "An internal server error occurred.", Details = ex.Message });
			}
		}
    }
}
