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
			var result = await _hallOfFameService.GetTopThreeDonorsAsync();
			return Ok(result);
		}

		[HttpGet("top-one-hundred-donors")]
		public async Task<IActionResult> GetTopOneHundredDonors()
		{
			var result = await _hallOfFameService.GetTopOneHundredDonors();
			return Ok(result);
		}
	}
}
