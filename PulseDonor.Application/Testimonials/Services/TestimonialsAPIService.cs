using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.City.DTO;
using PulseDonor.Application.Testimonials.Command;
using PulseDonor.Application.Testimonials.DTO;
using PulseDonor.Application.Testimonials.Interfaces;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Testimonials.Services
{
    public class TestimonialsAPIService : ITestimonialsAPIService
    {
        private readonly DevPulsedonorContext _context;
        public TestimonialsAPIService(DevPulsedonorContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(AddTestimonialCommand cmd)
        {
            var newTestimonial = new SuccessStory
            {
                Title = cmd.Title,
                Description = cmd.Description,
            };

            await _context.SuccessStories.AddAsync(newTestimonial);
            await _context.SaveChangesAsync();

            return newTestimonial.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (id == 0) return 0;
            var testimonial = await _context.SuccessStories
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            _context.SuccessStories.Remove(testimonial);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> EditAsync(EditTestimonialCommand cmd)
        {
            if (cmd.Id == 0) return 0;
            var testimonial = await _context.SuccessStories
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            testimonial.Title = cmd.Title;
            testimonial.Description = cmd.Description;

            await _context.SaveChangesAsync();

            return testimonial.Id;
        }

        public async Task<List<GetTestimonialDto>> GetAllAsync()
        {
            var testimonials = _context.SuccessStories.AsQueryable();

            return testimonials.Select(x => new GetTestimonialDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).ToList();
        }

        public async Task<GetTestimonialDto> GetByIdAsync(int id)
        {
            var testimonials = _context.SuccessStories.Where(x => x.Id == id).AsQueryable();

            return await testimonials.Select(x => new GetTestimonialDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).FirstOrDefaultAsync();

        }
    }
}
