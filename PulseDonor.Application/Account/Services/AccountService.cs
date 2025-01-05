using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.Account.Commands;
using PulseDonor.Application.Account.DTO;
using PulseDonor.Application.Account.Interfaces;
using PulseDonor.Application.CurrentUser.Interface;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.Services
{
	public class AccountService : IAccountService
	{
		private readonly DevPulsedonorContext _context;
		private readonly ICurrentUser _currentUser;

		public AccountService(DevPulsedonorContext context, ICurrentUser currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		public async Task<string> EditAccountOverviewAsync(EditAccountOverviewCommand cmd)
		{
			// Fetch user with related entities
			var user = await _context.Users
				.Include(x => x.BloodType)
				.Include(x => x.UserCities)
					.ThenInclude(x => x.City)
				.Where(x => x.Id == _currentUser.UserId)
				.FirstOrDefaultAsync();

			if (user == null)
			{
				throw new Exception("User not found.");
			}

			user.FirstName = cmd.FirstName;
			user.LastName = cmd.LastName;
			//user.Email = cmd.Email;
			user.IsActive = cmd.Status;
			user.BloodTypeId = cmd.BloodTypeId;

			if (cmd.PrimaryCityIdToDelete != null)
			{
				var previouslyUserPrimaryCity = await _context.UserCities
					.Where(x => x.IsPrimary && !x.IsDeleted && x.UserId == _currentUser.UserId && x.CityId == cmd.PrimaryCityIdToDelete)
					.FirstOrDefaultAsync();

				if (previouslyUserPrimaryCity != null)
				{
					previouslyUserPrimaryCity.IsDeleted = true;
				}

				var newPrimaryCity = new UserCity
				{
					UserId = _currentUser.UserId,
					CityId = cmd.PrimaryCityId,
					IsPrimary = true,
					InsertedFrom = _currentUser.UserId,
					InsertedDate = DateTime.UtcNow,
					IsDeleted = false
				};

				await _context.UserCities.AddAsync(newPrimaryCity);
			}

			if (cmd.SecondaryCityIdsToDelete != null)
			{
				var previouslyUserCities = await _context.UserCities
					.Where(x => x.UserId == _currentUser.UserId && !x.IsPrimary && !x.IsDeleted)
					.ToListAsync();

				foreach (var city in previouslyUserCities)
				{
					if (cmd.SecondaryCityIdsToDelete.Contains(city.CityId))
					{
						city.IsDeleted = true;
					}
				}
			}

			if (cmd.SecondaryCities != null)
			{
				foreach (var cityId in cmd.SecondaryCities)
				{
					var newSecondaryCity = new UserCity
					{
						UserId = _currentUser.UserId,
						CityId = cityId,
						IsPrimary = false,
						InsertedFrom = _currentUser.UserId,
						InsertedDate = DateTime.UtcNow,
						IsDeleted = false
					};

					await _context.UserCities.AddAsync(newSecondaryCity);
				}
			}

			await _context.SaveChangesAsync();

			return user.Id;
		}



		public async Task<SingleAccountOverviewDto> GetAccountOverviewAsync()
		{
			var user = await _context.Users
				.Include(x=>x.BloodType)
				.Include(x=>x.UserCities)
					.ThenInclude(x=>x.City)
				.Where(x => x.Id == _currentUser.UserId)
				.FirstOrDefaultAsync();

			var data = new SingleAccountOverviewDto
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				BloodType = new SingleBloodTypeDto { BloodTypeId = user.BloodTypeId, Type = user.BloodType.Type },
				PrimaryCity = user.UserCities.Where(x => x.IsPrimary && !x.IsDeleted).Select(x => new SinglePrimaryCityDto { PrimaryCityId = x.CityId, Name = x.City.Name }).FirstOrDefault(),
				SecondaryCities = user.UserCities.Where(x => !x.IsPrimary && !x.IsDeleted).Any()? 
				user.UserCities.Where(x => !x.IsPrimary && !x.IsDeleted).Select(x => new SingleSecondaryCityDto { SecondaryCityId = x.CityId, Name = x.City.Name }).ToList() : new List<SingleSecondaryCityDto>(),
				Status = user.IsActive,
			};

			return data;
		}
	}
}
