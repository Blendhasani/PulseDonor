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
        private readonly IBloodRequestService _bloodService;
        public BloodRequestsController(IBloodRequestService bloodService)
        {
            _bloodService = bloodService;
        }

        [HttpPost("add")]
        public async Task<int> Add(AddBloodeRequestCommand cmd)
        {
            var result = await _bloodService.AddAsync(cmd);
            return result;
        }

        [HttpGet()]
        public async Task<List<GetBloodRequestDto>> GetCities()
        {
            return await _bloodService.GetAllAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<GetBloodRequestDto> GetById(int id)
        {
            var result = await _bloodService.GetByIdAsync(id);
            return result;
        }

        [HttpPut("edit")]
        public async Task<int> Edit(EditBloodRequestCommand cmd)
        {
            var result = await _bloodService.EditAsync(cmd);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<int> Delete(int id)
        {
            var result = await _bloodService.DeleteAsync(id);
            return result;
        }
    }
}
