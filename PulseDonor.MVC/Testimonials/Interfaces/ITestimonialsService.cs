
using PulseDonor.MVC.Testimonials.Command;
using PulseDonor.MVC.Testimonials.DTO;

namespace PulseDonor.MVC.Testimonials.Interfaces
{
    public interface ITestimonialsService
    {
        Task<int> AddAsync(AddTestimonialAPICommand cmd);
        Task<List<GetTestimonialAPIDto>> GetAllAsync();
        Task<GetTestimonialAPIDto> GetByIdAsync(int id);
        Task<int> EditAsync(EditTestimonialAPICommand cmd);
        Task<int> DeleteAsync(int id);
    }
}
