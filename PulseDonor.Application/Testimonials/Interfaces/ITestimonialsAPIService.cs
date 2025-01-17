using PulseDonor.Application.Testimonials.Command;
using PulseDonor.Application.Testimonials.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Testimonials.Interfaces
{
    public interface ITestimonialsAPIService
    {
        Task<int> AddAsync(AddTestimonialCommand cmd);
        Task<List<GetTestimonialDto>> GetAllAsync();
        Task<GetTestimonialDto> GetByIdAsync(int id);
        Task<int> EditAsync(EditTestimonialCommand cmd);
        Task<int> DeleteAsync(int id);
    }
}
