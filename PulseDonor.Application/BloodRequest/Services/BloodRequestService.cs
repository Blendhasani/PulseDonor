using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.BloodDonationPoint.DTO;
using PulseDonor.Application.BloodRequest.Command;
using PulseDonor.Application.BloodRequest.DTO;
using PulseDonor.Application.BloodRequest.Interfaces;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodRequest.Services
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly DevPulsedonorContext _context;
        public BloodRequestService(DevPulsedonorContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(AddBloodeRequestCommand cmd)
        {
            var bloodPoint = new PulseDonor.Infrastructure.Models.BloodRequest
            {
               AuthorId = cmd.AuthorId,
               BloodTypeId = cmd.BloodTypeId,
               UrgenceTypeId = cmd.UrgencTypeId,
               HospitalId = cmd.HospitalId,
               DonorId = cmd.DonorId,
               FirstName = cmd.FirstName,
               LastName = cmd.LastName,
               Age = cmd.Age,
               Quantity = cmd.Quantity,
               PostKey = cmd.PostKey,
               DonationDate = cmd.DonationDate,
               DonationTime = cmd.DonationTime,
            };

            await _context.BloodRequests.AddAsync(bloodPoint);
            await _context.SaveChangesAsync();

            return bloodPoint.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (id == 0) return 0;

            var blood = await _context.BloodRequests
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            _context.BloodRequests.Remove(blood);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> EditAsync(EditBloodRequestCommand cmd)
        {
            if (cmd.Id == 0) return 0;
            var blood = await _context.BloodRequests
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            blood.AuthorId = cmd.AuthorId;
            blood.BloodTypeId = cmd.BloodTypeId;
            blood.UrgenceTypeId = cmd.UrgencTypeId;
            blood.HospitalId = cmd.HospitalId;
            blood.DonorId = cmd.DonorId;
            blood.FirstName = cmd.FirstName;
            blood.LastName = cmd.LastName;
            blood.Age = cmd.Age;
            blood.Quantity = cmd.Quantity;
            blood.PostKey = cmd.PostKey;
            blood.DonationDate = cmd.DonationDate;
            blood.DonationTime = cmd.DonationTime;

            await _context.SaveChangesAsync();

            return blood.Id;
        }

        public async Task<List<GetBloodRequestDto>> GetAllAsync()
        {
            var blood = _context.BloodRequests.AsQueryable();

            return blood.Select(x => new GetBloodRequestDto
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                BloodTypeId = x.BloodTypeId,
                UrgencTypeId = x.UrgenceTypeId,
                HospitalId = x.HospitalId,
                DonorId = x.DonorId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                Quantity = x.Quantity,
                PostKey = x.PostKey,
                DonationDate = x.DonationDate,
                DonationTime = x.DonationTime
            }).ToList();
        }

        public async Task<GetBloodRequestDto> GetByIdAsync(int id)
        {
            var bloodPoint = _context.BloodRequests.Where(x => x.Id == id).AsQueryable();

            var points = bloodPoint.Select(x => new GetBloodRequestDto
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                BloodTypeId = x.BloodTypeId,
                UrgencTypeId = x.UrgenceTypeId,
                HospitalId = x.HospitalId,
                DonorId = x.DonorId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                Quantity = x.Quantity,
                PostKey = x.PostKey,
                DonationDate = x.DonationDate,
                DonationTime = x.DonationTime
            }).FirstOrDefault();

            return points;
        }
    }
}
