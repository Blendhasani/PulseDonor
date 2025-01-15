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
        private readonly IBloodDonationPointsService _bloodService;
        public BloodDonationPointsController(IBloodDonationPointsService bloodService)
        {
            _bloodService = bloodService;
        }

        [HttpPost("Add")]
        public async Task<int> Add(AddBloodDonationPointCommand cmd)
        {
            var result = await _bloodService.AddAsync(cmd);
            return result;
        }

        [HttpGet()]
        public async Task<List<GetBloodDonationListDto>> GetCities()
        {
            return await _bloodService.GetAllAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<GetBloodDonationListDto> GetById(int id)
        {
            var result = await _bloodService.GetByIdAsync(id);
            return result;
        }

        [HttpPut("edit")]
        public async Task<int> Edit(EditBloodDonationPointCommand cmd)
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
