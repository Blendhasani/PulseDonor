using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.User.Commands;
using PulseDonor.Application.User.DTO;
using PulseDonor.Application.User.Interfaces;

namespace PulseDonor.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserController : Controller
	{
		private readonly IUserAPIService _userService;

		public UserController(IUserAPIService userService)
		{
			_userService = userService;
		}

		[HttpGet("GetList")]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _userService.GetUsersAsync();
			var result = new
			{
				data = users
			};
			return Ok(result);
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddUser([FromBody] AddUserAPICommand entity)
		{
			var addedUser = await _userService.AddUserAsync(entity);
			var result = new
			{
				data = addedUser
			};
			return Ok(result);
		}

		[HttpGet("Get")]
		public async Task<IActionResult> GetUserById(string id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			var result = new
			{
				data = user
			};
			return Ok(result);
		}

		[HttpPut("Edit")]
		public async Task<IActionResult> EditUser(EditUserAPICommand entity)
		{
			var editedUser = await _userService.EditUserAsync(entity);
			var result = new
			{
				data = editedUser
			};
			return Ok(result);
		}

		[HttpPut("UpdateIsBlocked")]
		public async Task<IActionResult> UpdateIsBlocked(UpdateIsBlockedUserAPICommand entity)
		{
			var user = await _userService.UpdateIsBlockedAsync(entity);
			var result = new
			{
				data = user
			};
			return Ok(result);
		}


		[HttpGet("ProfileComponent")]
		public async Task<ProfileComponentAPIDto> GetProfileComponent()
		{
			var result = await _userService.GetProfileComponentAsync();
			return result;
		}
	}
}
