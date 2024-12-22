using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.City.Commands;
using PulseDonor.Application.City.DTO;
using PulseDonor.Application.City.Interfaces;

namespace PulseDonor.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private readonly ICityAPIService _cityService;

		public CityController(ICityAPIService cityService)
		{
			_cityService = cityService;
		}

		[HttpPost("Add")]
		public async Task<int> AddCity(AddCityAPICommand entity)
		{
			var result = await _cityService.AddCityAsync(entity);
			return result;
		}

		[HttpGet("GetList")]
		public async Task<List<CitiesAPIDto>> GetCities()
		{
			var result = await _cityService.GetCitiesAsync();
			return result;
		}


		[HttpGet("Get")]
		public async Task<SingleCityAPIDto> GetCityById(int id)
		{
			var result = await _cityService.GetCityByIdAsync(id);
			return result;
		}

		[HttpPut("Edit")]
		public async Task<int> EditCity(EditCityAPICommand entity)
		{
			var result = await _cityService.EditCityAsync(entity);
			return result;
		}


		[HttpDelete("Delete")]
		public async Task<int> DeleteCity(int id)
		{
			var result = await _cityService.DeleteCityAsync(id);
			return result;
		}

	}
}
