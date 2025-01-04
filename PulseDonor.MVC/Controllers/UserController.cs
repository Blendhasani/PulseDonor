using Microsoft.AspNetCore.Mvc;
using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.User.Commands;
using PulseDonor.MVC.User.Interfaces;

namespace PulseDonor.MVC.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> GetUsers()
		{
			var result = await _userService.GetUsers();
			return Json(result);
		}

		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Add([FromForm] AddUserCommand model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _userService.AddUser(model);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(string id)
		{
			var result = await _userService.GetUserById(id);
			return View("Edit", result);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditUserCommand model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _userService.EditUser(model);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateIsBlocked(string userId, bool isBlocked)
		{
			var command = new UpdateIsBlockedUserCommand
			{
				UserId = userId,
				IsBlocked = isBlocked
			};
			var result = await _userService.UpdateIsBlocked(command);
			return RedirectToAction("Index");

			//try
			//{
			//	// 1) Use userId to fetch the user from the database
			//	// 2) Update the isBlocked status
			//	// 3) Save changes, e.g., _dbContext.SaveChanges();

			//	return Json(new { success = true, message = "Status updated successfully." });
			//}
			//catch (Exception ex)
			//{
			//	// Log exception
			//	return Json(new { success = false, error = ex.Message });
			//}
		}

		public async Task<IActionResult>ProfileComponent()
		{
			var result = await _userService.GetProfileComponent();
			return PartialView("_ProfilePartial", result);
		}
	}
}
