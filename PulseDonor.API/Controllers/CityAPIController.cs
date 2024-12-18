using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.City.Commands;

namespace PulseDonor.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CityAPIController : ControllerBase
	{
		[HttpPost("addCity")]
		public async Task<int> AddAsync(AddCityAPICommand entity)
		{
			var test = 1;
			return await Task.FromResult(test);
		}

	}
}
