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
			var bloodTypes = await _dataService.GetBloodTypes();
			var result = new
			{
				data = bloodTypes
			};
			return Ok(result);
		}

		[HttpGet("users")]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _dataService.GetUsers();
			var result = new
			{
				data = users
			};
			return Ok(result);
		}

		[HttpGet("cities")]
		public async Task<IActionResult> GetCities()
		{
			var cities = await _dataService.GetCities();
			var result = new
			{
				data = cities
			};
			return Ok(result);
		}

		[HttpGet("urgence-types")]
		public async Task<IActionResult> GetUrgenceTypes()
		{
			var urgenceTypes = await _dataService.GetUrgenceTypes();
			var result = new
			{
				data = urgenceTypes
			};
			return Ok(result);
		}


		[HttpGet("hospitals")]
		public async Task<IActionResult> GetHospitals()
		{
			var hospitals = await _dataService.GetHospitals();
			var result = new
			{
				data = hospitals
			};
			return Ok(result);
		}
	}
}
