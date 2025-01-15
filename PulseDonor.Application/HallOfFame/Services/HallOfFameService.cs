using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.CurrentUser.Interface;
using PulseDonor.Application.HallOfFame.DTO;
using PulseDonor.Application.HallOfFame.Interfaces;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.HallOfFame.Services
{
	public class HallOfFameService : IHallOfFameService
	{
		private readonly DevPulsedonorContext _context;
		private readonly ICurrentUser _currentUser;

		public HallOfFameService(DevPulsedonorContext context, ICurrentUser currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		public async Task<List<TopThreeDonorsDto>> GetTopThreeDonorsAsync()
		{
			var bloodRequests = _context.BloodRequests.Include(x=>x.Donor).Where(x => !x.IsDeleted).AsQueryable();
			
			var response = await bloodRequests
					.GroupBy(x => x.DonorId)
					.Select(g => new TopThreeDonorsDto
					{
						FullName = g.FirstOrDefault().Donor.FirstName + " " + g.FirstOrDefault().Donor.LastName,
						Count = g.Count()
					})
					.OrderByDescending(d => d.Count)
					.Take(3)
					.ToListAsync();

			return response;

		}

		public async Task<List<TopOneHundredDonorsDto>> GetTopOneHundredDonorsAsync()
		{
			var bloodRequests = _context.BloodRequests
				.Include(x => x.Donor)
				.Where(x => !x.IsDeleted)
				.AsQueryable();

			var groupedDonors = await bloodRequests
				.GroupBy(x => x.DonorId)
				.Select(g => new
				{
					DonorId = g.Key,
					FullName = g.FirstOrDefault().Donor.FirstName + " " + g.FirstOrDefault().Donor.LastName,
					Count = g.Count()
				})
				.OrderByDescending(g => g.Count)
				.ToListAsync();

			var topOneHundred = groupedDonors
				.Take(5)
				.Select((g, index) => new OneHundredDonorsDto
				{
					FullName = g.FullName,
					Count = g.Count
				})
				.ToList();

			var currentUser = _currentUser.UserId; 
			var currentUserPosition = groupedDonors
				.Select((g, index) => new
				{
					Position = index + 1,
					g.DonorId
				})
				.FirstOrDefault(x => x.DonorId == currentUser)?.Position ?? -1;

			return new List<TopOneHundredDonorsDto>
	{
		new TopOneHundredDonorsDto
		{
			MyPosition = currentUserPosition,
			Donors = topOneHundred
		}
	};
		}

		public async Task<List<BloodTypesChartDto>> GetBloodTypesChartAsync()
		{
			var users = _context.Users.Where(x => !x.IsBlocked).AsQueryable();

			int total = users.Count();

			var groupedData = users
				.GroupBy(user => user.BloodType.Type)
				.Select(group => new BloodTypesChartDto()
				{
					BloodType = group.Key, 
					Count = group.Count(),
					Percentage = (group.Count() / (double)total) * 100
				})
				.ToList();

			return groupedData;

		}
	}
}
