using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.BloodDonationPoint.DTO;
using PulseDonor.Application.Hospitals.Commands;
using PulseDonor.Application.Hospitals.DTO;
using PulseDonor.Application.Hospitals.Interfaces;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Hospitals.Services
{
    public class HospitalAPIService : IHospitalAPIService
    {
        private readonly DevPulsedonorContext _context;
        public HospitalAPIService(DevPulsedonorContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(AddHospitalCommand cmd)
        {
            var hospital = new PulseDonor.Infrastructure.Models.Hospital
            {
                CityId = cmd.CityId,
                Name = cmd.Name,
                Address = cmd.Address,
            };

            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();

            return hospital.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (id == 0) return 0;

            var hospital = await _context.Hospitals
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> EditAsync(EditHospitalCommand cmd)
        {
            if (cmd.Id == 0) return 0;
            var hospital = await _context.Hospitals
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            hospital.Address = cmd.Address;
            hospital.CityId = cmd.CityId;
            hospital.Name = cmd.Name;

            await _context.SaveChangesAsync();

            return hospital.Id;
        }

        public async Task<List<GetHospitalsDto>> GetAllAsync()
        {
            var hospital = _context.Hospitals.AsQueryable();

            return hospital.Select(x => new GetHospitalsDto
            {
                Id = x.Id,
                CityId = x.CityId,
                Address = x.Address,
                City = x.City.Name,
                Name = x.Name,
            }).ToList();
        }

        public async Task<GetHospitalDto> GetByIdAsync(int id)
        {
            var hospitals = _context.Hospitals.Where(x => x.Id == id).AsQueryable();

            var hospital = hospitals.Select(x => new GetHospitalDto
            {
                Id = x.Id,
                Address = x.Address,
                CityId = x.CityId,
                Name = x.Name,
                City = x.City.Name
                
            }).FirstOrDefault();

            return hospital;
        }
    }
}
