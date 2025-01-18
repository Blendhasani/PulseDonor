using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PulseDonor.Application.Testimonials.Command;
using PulseDonor.Application.Testimonials.Interfaces;
using PulseDonor.Application.Testimonials.Command;
using PulseDonor.Application.Testimonials.Interfaces;

namespace PulseDonor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {

        private readonly ITestimonialsAPIService _service;
        public TestimonialsController(ITestimonialsAPIService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddTestimonialCommand cmd)
        {
            var addedTestimonial = await _service.AddAsync(cmd);
            var result = new
            {
                data = addedTestimonial
            };
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetTestimonials()
        {
            var Testimonials = await _service.GetAllAsync();
            var result = new
            {
                data = Testimonials
            };
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var Testimonial = await _service.GetByIdAsync(id);
            var result = new
            {
                data = Testimonial
            };
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditTestimonialCommand cmd)
        {
            var editedTestimonial = await _service.EditAsync(cmd);
            var result = new
            {
                data = editedTestimonial
            };
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTestimonial = await _service.DeleteAsync(id);
            var result = new
            {
                data = deletedTestimonial
            };
            return Ok(result);
        }
    }
}
