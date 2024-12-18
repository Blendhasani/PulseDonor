using Microsoft.AspNetCore.Mvc;

namespace PulseDonor.MVC.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
