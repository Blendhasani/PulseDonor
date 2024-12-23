using Microsoft.AspNetCore.Mvc;
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
	}
}
