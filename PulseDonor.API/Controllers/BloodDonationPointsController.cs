using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.BloodDonationPoint.Commands;
using PulseDonor.Application.BloodDonationPoint.DTO;
using PulseDonor.Application.BloodDonationPoint.Interfaces;
using PulseDonor.Application.City.Commands;
using PulseDonor.Application.City.DTO;
using PulseDonor.Application.City.Interfaces;
using PulseDonor.Application.CurrentUser.Interface;

namespace PulseDonor.API.Controllers
{
    [Route("api/blood-donation-points")]
    [ApiController]
    public class BloodDonationPointsController : ControllerBase
    {
        private readonly IBloodDonationPointsAPIService _bloodService;
        public BloodDonationPointsController(IBloodDonationPointsAPIService bloodService)
        {
            _bloodService = bloodService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddBloodDonationPointCommand cmd)
        {
            var addedBloodDonation = await _bloodService.AddAsync(cmd);
			var result = new
			{
				data = addedBloodDonation
			};
			return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetBloodDonations()
        {
            var bloodDonations = await _bloodService.GetAllAsync();
			var result = new
			{
				data = bloodDonations
			};
            return Ok(result);

		}

        [HttpGet("Get")]
        public async Task<IActionResult> GetById(int id)
        {
            var bloodDonationPoint = await _bloodService.GetByIdAsync(id);
			var result = new
			{
				data = bloodDonationPoint
			};
			return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditBloodDonationPointCommand cmd)
        {
            var editedBloodDonationPoint = await _bloodService.EditAsync(cmd);
			var result = new
			{
				data = editedBloodDonationPoint
			};
			return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedBloodDonationPoint = await _bloodService.DeleteAsync(id);
			var result = new
			{
				data = deletedBloodDonationPoint
			};
			return Ok(result);
        }
    }
}
