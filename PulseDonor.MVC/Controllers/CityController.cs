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

		public async Task<IActionResult> GetCities()
		{
			var result = await _cityService.GetCities();
			return Json(result);
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

			var result = await _cityService.AddCity(model);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var result = await _cityService.GetCityById(id);
			return PartialView("_Edit" , result);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditCityCommand model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _cityService.EditCity(model);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			if(id == 0)
				return RedirectToAction("Index");

			var result = await _cityService.DeleteCity(id);

			return RedirectToAction("Index");

		}

	}
}
