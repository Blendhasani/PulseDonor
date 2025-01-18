using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.BloodDonationPoint.DTO;
using PulseDonor.Application.BloodRequest.Command;
using PulseDonor.Application.BloodRequest.DTO;
using PulseDonor.Application.BloodRequest.Interfaces;
using PulseDonor.Application.CurrentUser.Interface;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodRequest.Services
{
    public class BloodRequestAPIService : IBloodRequestAPIService
    {
        private readonly DevPulsedonorContext _context;
        private readonly ICurrentUser _currentUser;

        public BloodRequestAPIService(DevPulsedonorContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<int> AddAsync(AddBloodeRequestCommand cmd)
        {
            var bloodPoint = new PulseDonor.Infrastructure.Models.BloodRequest
            {
               BloodTypeId = cmd.BloodTypeId,
               UrgenceTypeId = cmd.UrgencTypeId,
               HospitalId = cmd.HospitalId,
               FirstName = cmd.FirstName,
               LastName = cmd.LastName,
               Age = cmd.Age,
               Quantity = cmd.Quantity,
               DonationDate = cmd.DonationDate,
               DonationTime = cmd.DonationTime,
               InsertedDate = DateTime.Now,
               InsertedFrom = _currentUser.UserId,
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

        public async Task<int> SendRequest(int id)
        {
            var bloodReq = new PulseDonor.Infrastructure.Models.BloodRequestApplication
            {
                UserId = _currentUser.UserId,
                BloodRequestId = id,
                InsertedDate = DateTime.Now,
                InsertedFrom = _currentUser.UserId
            };

            await _context.BloodRequestApplications.AddAsync(bloodReq);
            await _context.SaveChangesAsync();

            return bloodReq.Id;
        }

        public async Task<int> EditAsync(EditBloodRequestCommand cmd)
        {
            if (cmd.Id == 0) return 0;
            var blood = await _context.BloodRequests
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();
            if(blood is null)
            {
                return 0;
            }
            blood.BloodTypeId = cmd.BloodTypeId;
            blood.UrgenceTypeId = cmd.UrgenceTypeId;
            blood.HospitalId = cmd.HospitalId;
            blood.FirstName = cmd.FirstName;
            blood.LastName = cmd.LastName;
            blood.Age = cmd.Age;
            blood.Quantity = cmd.Quantity;
            blood.DonationDate = cmd.DonationDate;
            blood.DonationTime = cmd.DonationTime;

            await _context.SaveChangesAsync();

            return blood.Id;
        }

        public async Task<List<GetBloodRequestDto>> GetAllAsync()
        {
            var blood = _context.BloodRequests
                .Include(x=>x.BloodType)
                .Include(x=>x.UrgenceType)
                .Include(x=>x.Hospital)
                .Include(x=>x.Author)
                .AsQueryable();

            return blood.Select(x => new GetBloodRequestDto
            {
                Id = x.Id,
				Author = new SingleBloodRequestAuthor
				{
					Id = x.AuthorId,
					Name = x.Author.FirstName + " " + x.Author.LastName
				},
				BloodType = new SingleBloodRequestBloodType
                {
                    Id = x.BloodTypeId,
                    Type = x.BloodType.Type
                },
                UrgenceType = new SingleBloodRequestUrgence
                {
                    Id = x.UrgenceTypeId,
                    Type = x.UrgenceType.Type
				},
				Hospital = x.HospitalId != null ? new SingleBloodRequestHospital
				{
					Id = x.HospitalId,
					Name = x.Hospital.Name
				} : null,
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
            var bloodPoint = _context.BloodRequests
                .Include(x => x.BloodType)
				.Include(x => x.UrgenceType)
				.Include(x => x.Hospital)
				.Include(x => x.Author).Where(x => x.Id == id).AsQueryable();

            var points = bloodPoint.Select(x => new GetBloodRequestDto
            {
                Id = x.Id,
                Author = new SingleBloodRequestAuthor
                {
                    Id = x.AuthorId,
                    Name = x.Author.FirstName + " " + x.Author.LastName
                },
				BloodType = new SingleBloodRequestBloodType
				{
					Id = x.BloodTypeId,
					Type = x.BloodType.Type
				},
				UrgenceType = new SingleBloodRequestUrgence
				{
					Id = x.UrgenceTypeId,
					Type = x.UrgenceType.Type
				},
				Hospital = x.HospitalId != null? new SingleBloodRequestHospital
				{
					Id = x.HospitalId,
					Name = x.Hospital.Name
				} : null,
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
