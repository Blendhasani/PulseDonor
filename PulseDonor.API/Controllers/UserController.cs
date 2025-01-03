using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.User.Commands;
using PulseDonor.Application.User.DTO;
using PulseDonor.Application.User.Interfaces;

namespace PulseDonor.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserAPIService _userService;

		public UserController(IUserAPIService userService)
		{
			_userService = userService;
		}

		[HttpGet("GetList")]
		public async Task<List<UsersAPIDto>> GetUsers()
		{
			var result = await _userService.GetUsersAsync();
			return result;
		}

		[HttpPost("Add")]
		public async Task<string> AddUser([FromBody] AddUserAPICommand entity)
		{
			var result = await _userService.AddUserAsync(entity);
			return result;
		}

		[HttpGet("Get")]
		public async Task<SingleUserAPIDto> GetUserById(string id)
		{
			var result = await _userService.GetUserByIdAsync(id);
			return result;
		}

		[HttpPut("Edit")]
		public async Task<int> EditUser(EditUserAPICommand entity)
		{
			var result = await _userService.EditUserAsync(entity);
			return result;
		}

	}
}
