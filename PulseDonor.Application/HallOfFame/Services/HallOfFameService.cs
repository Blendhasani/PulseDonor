using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.CurrentUser.Interface;
using PulseDonor.Application.HallOfFame.Commands;
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

		public async Task<int> CreateGroupAsync(AddGroupCommand cmd)
		{
			var newGroup = new Group
			{
				CityId = cmd.CityId,
				Name = cmd.Name,
				Description = cmd.Description,
				InsertedDate = DateTime.UtcNow,
				InsertedFrom = _currentUser.UserId,
				IsDeleted = false
			};

			await _context.Groups.AddAsync(newGroup);
			await _context.SaveChangesAsync();
			return newGroup.Id;
		}

		public async Task<string> CreateJoinCodeAsync(int groupId)
		{
			var group = await _context.Groups.FindAsync(groupId);
			if (group == null || group.IsDeleted)
				throw new Exception("Grupi nuk ekziston ose është fshirë.");

			var groupCodeValid = await _context.GroupMemberJoinCodes
				.AnyAsync(gm => gm.GroupId == groupId && gm.MemberId == _currentUser.UserId && !gm.IsDeleted && gm.ExpiracyDate > DateTime.UtcNow);
			if (groupCodeValid)
				throw new Exception("Nuk mund te krijoni kod pasi kodi juaj eshte ende valid.");

			string joinCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

			bool codeExists = await _context.GroupMemberJoinCodes
				.AnyAsync(jc => jc.Code == joinCode && !jc.IsDeleted);
			if (codeExists)
				joinCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

			var joinCodeEntry = new GroupMemberJoinCode
			{
				MemberId = _currentUser.UserId,
				GroupId = groupId,
				Code = joinCode,
				ExpiracyDate = DateTime.UtcNow.AddHours(48),
				IsDeleted = false,
				InsertedFrom = _currentUser.UserId,
				InsertedDate = DateTime.UtcNow
			};

			_context.GroupMemberJoinCodes.Add(joinCodeEntry);
			await _context.SaveChangesAsync();

			return joinCode;
		}

		public async Task<string> JoinGroupAsync(int groupId, JoinGroupCommand cmd)
		{
			var group = await _context.Groups.FindAsync(groupId);
			if (group == null || group.IsDeleted)
				return "Grupi nuk ekziston ose është fshirë.";

			var groupCode =  _context.GroupMemberJoinCodes
				.Where(gm => gm.GroupId == groupId && gm.Code.Equals(cmd.JoinCode) && !gm.IsDeleted).FirstOrDefault();

			if (groupCode == null)
				return "Grupi nuk ekziston apo eshte fshire";

			if (groupCode.ExpiracyDate < DateTime.UtcNow)
				return "Kodi juaj ka skaduar!";

			var newGroupMember = new GroupMember
			{
				GroupId = group.Id,
				MemberId = _currentUser.UserId,
				InsertedFrom = groupCode.MemberId,
				InsertedDate = DateTime.UtcNow,
				IsDeleted = false
			};

			await _context.GroupMembers.AddAsync(newGroupMember);
			await _context.SaveChangesAsync();
			return "Kërkesa u realizua!";
		}
	}
}
