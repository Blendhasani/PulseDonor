using Microsoft.AspNetCore.Mvc;
using PulseDonor.MVC.Auth.Commands;
using PulseDonor.MVC.Auth.Interfaces;

namespace PulseDonor.MVC.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		public IActionResult Login()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return View(new LoginCommand());
			}
			return View("~/Views/User/Index.cshtml");
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginCommand cmd)
		{
			var result = await _authService.Login(cmd);
			return RedirectToAction("Index", "User");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			var result = await _authService.Logout();
			return RedirectToAction("Login", "Auth");

		}

	}
}
