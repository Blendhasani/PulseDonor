using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.BloodRequest.Command;
using PulseDonor.Application.BloodRequest.Interfaces;
using PulseDonor.Application.Hospitals.Commands;
using PulseDonor.Application.Hospitals.Interfaces;

namespace PulseDonor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalAPIService _service;
        public HospitalController(IHospitalAPIService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddHospitalCommand cmd)
        {
            var addedBloodRequest = await _service.AddAsync(cmd);
            var result = new
            {
                data = addedBloodRequest
            };
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bloodRequests = await _service.GetAllAsync();
            var result = new
            {
                data = bloodRequests
            };
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var bloodRequest = await _service.GetByIdAsync(id);
            var result = new
            {
                data = bloodRequest
            };
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditHospitalCommand cmd)
        {
            var editedBloodRequest = await _service.EditAsync(cmd);
            var result = new
            {
                data = editedBloodRequest
            };
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedBloodRequest = await _service.DeleteAsync(id);
            var result = new
            {
                data = deletedBloodRequest
            };
            return Ok(result);
        }


    }
}
