using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.HallOfFame.Interfaces;

namespace PulseDonor.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class HallOfFameController : ControllerBase
	{
		private readonly IHallOfFameService _hallOfFameService;

		public HallOfFameController(IHallOfFameService hallOfFameService)
		{
			_hallOfFameService = hallOfFameService;
		}

		[HttpGet("top-three-donors")]
		public async Task<IActionResult> GetTopThreeDonors()
		{
			var donors = await _hallOfFameService.GetTopThreeDonorsAsync();
			var result = new
			{
				data = donors
			};
			return Ok(result);
		}

		[HttpGet("top-one-hundred-donors")]
		public async Task<IActionResult> GetTopOneHundredDonors()
		{
			var donors = await _hallOfFameService.GetTopOneHundredDonorsAsync();
			var result = new
			{
				data = donors
			};
			return Ok(result);
		}

		[HttpGet("blood-types-chart")]
		public async Task<IActionResult> GetBloodTypesChart()
		{
			var bloodTypes = await _hallOfFameService.GetBloodTypesChartAsync();
			var result = new
			{
				data = bloodTypes
			};
			return Ok(result);
		}
	}
}
