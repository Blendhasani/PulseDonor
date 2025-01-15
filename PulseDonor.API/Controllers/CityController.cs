using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.City.Commands;
using PulseDonor.Application.City.DTO;
using PulseDonor.Application.City.Interfaces;
using PulseDonor.Application.CurrentUser.Interface;

namespace PulseDonor.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private readonly ICityAPIService _cityService;
        private readonly ICurrentUser _currentUser;
        public CityController(ICityAPIService cityService, ICurrentUser currentUser)
		{
			_cityService = cityService;
            _currentUser = currentUser;
        }

		[HttpPost("Add")]
		public async Task<IActionResult> AddCity(AddCityAPICommand entity)
		{
			var addedCity = await _cityService.AddCityAsync(entity);
			var result = new
			{
				data = addedCity
			};
			return Ok(result);
		}

        [HttpGet("GetList")]
		public async Task<IActionResult> GetCities()
		{
            var cities = await _cityService.GetCitiesAsync();
			var result = new
			{
				data = cities
			};
			return Ok(result);
		}


		[HttpGet("Get")]
		public async Task<IActionResult> GetCityById(int id)
		{
			var city = await _cityService.GetCityByIdAsync(id);
			var result = new
			{
				data = city
			};
			return Ok(result);
		}

		[HttpPut("Edit")]
		public async Task<IActionResult> EditCity(EditCityAPICommand entity)
		{
			var editedCity = await _cityService.EditCityAsync(entity);
			var result = new
			{
				data = editedCity
			};
			return Ok(result);
		}


		[HttpDelete("Delete")]
		public async Task<IActionResult> DeleteCity(int id)
		{
			var deletedCity = await _cityService.DeleteCityAsync(id);
			var result = new
			{
				data = deletedCity
			};
			return Ok(result);
		}

	}
}
