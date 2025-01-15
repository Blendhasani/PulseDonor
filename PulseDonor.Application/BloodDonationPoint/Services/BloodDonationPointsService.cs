using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.BloodDonationPoint.Commands;
using PulseDonor.Application.BloodDonationPoint.DTO;
using PulseDonor.Application.BloodDonationPoint.Interfaces;
using PulseDonor.Application.City.DTO;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodDonationPoint.Services
{
    public class BloodDonationPointsService : IBloodDonationPointsService
    {
        private readonly DevPulsedonorContext _context;
        public BloodDonationPointsService(DevPulsedonorContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(AddBloodDonationPointCommand cmd)
        {
            var bloodPoint = new PulseDonor.Infrastructure.Models.BloodDonationPoint
            {
                Address = cmd.Address,
                StartTime = cmd.StartTime,
                EndTime = cmd.EndTime,
                Longitude = cmd.Longitude,
                Latitude = cmd.Latitude
            };

            await _context.BloodDonationPoints.AddAsync(bloodPoint);
            await _context.SaveChangesAsync();

            return bloodPoint.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (id == 0) return 0;

            var bloodPoint = await _context.BloodDonationPoints
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            _context.BloodDonationPoints.Remove(bloodPoint);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> EditAsync(EditBloodDonationPointCommand cmd)
        {
            if (cmd.Id == 0) return 0;
            var bloodPoint = await _context.BloodDonationPoints
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            bloodPoint.Address = cmd.Address;
            bloodPoint.StartTime = cmd.StartTime;
            bloodPoint.EndTime = cmd.EndTime;
            bloodPoint.Longitude = cmd.Longitude;
            bloodPoint.Latitude = cmd.Latitude;

            await _context.SaveChangesAsync();

            return bloodPoint.Id;
        }

        public async Task<List<GetBloodDonationListDto>> GetAllAsync()
        {
            var bloodPoint = _context.BloodDonationPoints.AsQueryable();

            return bloodPoint.Select(x => new GetBloodDonationListDto
            {
                Id = x.Id,
                Address = x.Address,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Longitude = x.Longitude,
                Latitude = x.Latitude
            }).ToList();
        }

        public async Task<GetBloodDonationListDto> GetByIdAsync(int id)
        {
            var bloodPoint = _context.BloodDonationPoints.Where(x => x.Id == id).AsQueryable();

            var points = bloodPoint.Select(x => new GetBloodDonationListDto
            {
                Id = x.Id,
                Address = x.Address,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Longitude = x.Longitude,
                Latitude = x.Latitude
            }).FirstOrDefault();

            return points;
        }
    }
}
