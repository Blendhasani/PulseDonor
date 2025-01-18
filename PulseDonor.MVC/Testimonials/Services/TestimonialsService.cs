using PulseDonor.Application.Hospitals.DTO;
using PulseDonor.MVC.Helper.Interfaces;
using PulseDonor.MVC.Hospitals.Commands;
using PulseDonor.MVC.Testimonials.Command;
using PulseDonor.MVC.Testimonials.DTO;
using PulseDonor.MVC.Testimonials.Interfaces;

namespace PulseDonor.MVC.Testimonials.Services
{
    public class TestimonialsService : ITestimonialsService
    {
        private readonly IApiClientHelper _apiClientHelper;

        public TestimonialsService(IApiClientHelper apiClientHelper)
        {
            _apiClientHelper = apiClientHelper;
        }

        public async Task<int> AddAsync(AddTestimonialAPICommand cmd)
        {
            string url = "https://localhost:7269/api/Testimonials/Add";
            return await _apiClientHelper.PostAsync<AddTestimonialAPICommand, int>(url, cmd);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string url = "https://localhost:7269/api/Testimonials/Delete?id=" + id;
            return await _apiClientHelper.DeleteAsync<int>(url);
        }

        public async Task<int> EditAsync(EditTestimonialAPICommand cmd)
        {
            string url = "https://localhost:7269/api/City/Edit";
            return await _apiClientHelper.PutAsync<EditTestimonialAPICommand, int>(url, cmd);
        }

        public async Task<List<GetTestimonialAPIDto>> GetAllAsync()
        {
            string url = "https://localhost:7269/api/Testimonials/GetAll";
            var data = await _apiClientHelper.GetAsync<List<GetTestimonialAPIDto>>(url);

            return data.Select(x => new GetTestimonialAPIDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description

            }).ToList();
        }

        public async Task<GetTestimonialAPIDto> GetByIdAsync(int id)
        {
            string url = "https://localhost:7269/api/Testimonials/GetById";

            var data = await _apiClientHelper.GetByIdAsync<GetTestimonialAPIDto>(url, id);

            var singleCity = new GetTestimonialAPIDto
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description
            };

            return singleCity;
        }
    }
}
