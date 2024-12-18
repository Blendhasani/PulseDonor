using Microsoft.AspNetCore.Mvc;
using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.City.Interfaces;

namespace PulseDonor.MVC.Controllers
{
	public class CityController : Controller
	{
		private readonly ICityService _cityService;

		public CityController(ICityService cityService)
		{
			_cityService = cityService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Add()
		{
			return PartialView("_Add");
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddCityCommand model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// Here we rely on the FrontService to use the URL and API helper to call the API
			var result = await _cityService.AddCity(model);

			return RedirectToAction("Index");
		}

		public IActionResult Edit()
		{
			return PartialView("_Edit");
		}

	}
}
