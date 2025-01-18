using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.BloodDonationPoint.Commands;
using PulseDonor.Application.BloodDonationPoint.DTO;
using PulseDonor.Application.BloodDonationPoint.Interfaces;
using PulseDonor.Application.BloodRequest.Command;
using PulseDonor.Application.BloodRequest.DTO;
using PulseDonor.Application.BloodRequest.Interfaces;

namespace PulseDonor.API.Controllers
{
    [Route("api/blood-request")]
    [ApiController]
    public class BloodRequestsController : ControllerBase
    {
        private readonly IBloodRequestAPIService _bloodService;
        public BloodRequestsController(IBloodRequestAPIService bloodService)
        {
            _bloodService = bloodService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddBloodeRequestCommand cmd)
        {
            var addedBloodRequest = await _bloodService.AddAsync(cmd);
			var result = new
			{
				data = addedBloodRequest
			};
			return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetBloodRequests()
        {
            var bloodRequests = await _bloodService.GetAllAsync();
			var result = new
			{
				data = bloodRequests
			};
            return Ok(result);
		}

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bloodRequest = await _bloodService.GetByIdAsync(id);
			var result = new
			{
				data = bloodRequest
			};
			return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditBloodRequestCommand cmd)
        {
            var editedBloodRequest = await _bloodService.EditAsync(cmd);
			var result = new
			{
				data = editedBloodRequest
			};
			return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedBloodRequest = await _bloodService.DeleteAsync(id);
			var result = new
			{
				data = deletedBloodRequest
			};
			return Ok(result);
        }

        [HttpPost("apply-for-request/{id}")]
        public async Task<IActionResult> ApplyForRequest(int id)
        {
            var appliedRequest = await _bloodService.ApplyForRequestAsync(id);

			var result = new
			{
				data = appliedRequest
			};
			return Ok(result);
		}
    }
}
