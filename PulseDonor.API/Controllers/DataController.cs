using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.Data.Interfaces;

namespace PulseDonor.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataController : Controller
	{
		private readonly IDataAPIService _dataService;
		public DataController(IDataAPIService dataService)
		{
			_dataService = dataService;
		}

		[HttpGet("blood-types")]
		public async Task<IActionResult> GetBloodTypes()
		{
			var result = await _dataService.GetBloodTypes();
			return Ok(result);
		}

		[HttpGet("users")]
		public async Task<IActionResult> GetUsers()
		{
			var result = await _dataService.GetUsers();
			return Ok(result);
		}

		[HttpGet("cities")]
		public async Task<IActionResult> GetCities()
		{
			var result = await _dataService.GetCities();
			return Ok(result);
		}

	}
}
