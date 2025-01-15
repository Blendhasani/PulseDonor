using PulseDonor.Application.City.DTO;
using PulseDonor.Application.User.DTO;
using PulseDonor.Application.User.Interfaces;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PulseDonor.Application.User.Commands;
using System.Security.Cryptography;
using PulseDonor.Application.City.Commands;
using PulseDonor.Application.CurrentUser.Interface;


namespace PulseDonor.Application.User.Services
{
    public class UserAPIService : IUserAPIService
	{
		private readonly DevPulsedonorContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICurrentUser _currentUser;


		public UserAPIService(DevPulsedonorContext context, UserManager<ApplicationUser> userManager, ICurrentUser currentUser)
		{
			_context = context;
			_userManager = userManager;
			_currentUser = currentUser;
		}

		public async Task<string> AddUserAsync(AddUserAPICommand cmd)
		{
			var user = new ApplicationUser()
			{
				FirstName = cmd.FirstName,
				LastName = cmd.LastName,
				Email = cmd.Email,
				ImagePath = null,
				UserName = cmd.Email.Trim(),
				EmailConfirmed = true,
				InsertedDate = DateTime.UtcNow,
				BloodTypeId = 1, //default for admin
				GenderId = 1, //default for admin
			};

			var result = await _userManager.CreateAsync(user, cmd.Password);
		
			if (!result.Succeeded)
			{
				return "Request Failed";
			}

			await _userManager.AddToRoleAsync(user, "Admin");
			return user.Id;
		}

		public async Task<SingleUserAPIDto> GetUserByIdAsync(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return new SingleUserAPIDto();
			}

			var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

			var response = new SingleUserAPIDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email
			};

			return response;

		}

		public async Task<List<UsersAPIDto>> GetUsersAsync()
		{
			var usersQuery = _context.Users
				.OrderByDescending(x=>x.InsertedDate)
				.AsQueryable();
			var users = await usersQuery
			   .Select(x => new
			   {
				   x.Id,
				   Fullname = x.FirstName + " " + x.LastName,
				   Email = x.Email,
				   Gender = x.Gender.Type,
				   Birthdate = x.Birthdate,
				   BloodType = x.BloodType.Type,
				   IsBlocked = x.IsBlocked,
				   InsertedDate = x.InsertedDate != null ? x.InsertedDate.Value.ToString("dd/MM/yyyy") : "N/A"
			   })
			   .ToListAsync();

			var usersWithRoles = new List<UsersAPIDto>();

			foreach (var user in users)
			{
				var identityUser = await _userManager.FindByIdAsync(user.Id);
				var roles = identityUser != null ? await _userManager.GetRolesAsync(identityUser) : new List<string>();

				usersWithRoles.Add(new UsersAPIDto
				{
					Id = user.Id,
					Fullname = user.Fullname,
					Email = user.Email,
					Gender = user.Gender,
					Age = user.Birthdate != null ? DateTime.Now.Year - user.Birthdate.Value.Year : null,
					BloodType = user.BloodType,
					Role = roles.FirstOrDefault() ?? "No Role",
					IsBlocked = user.IsBlocked,
					InsertedDate = user.InsertedDate
				});
			}

			return usersWithRoles;
		}


		public async Task<int> EditUserAsync(EditUserAPICommand cmd)
		{
			if (string.IsNullOrEmpty(cmd.Id)) return 0;
			var user = await _context.Users
				.Where(x => x.Id == cmd.Id)
				.FirstOrDefaultAsync();

			user.FirstName = cmd.FirstName;
			user.LastName = cmd.LastName;
			user.Email = cmd.Email;
			user.NormalizedEmail = cmd.Email?.ToUpperInvariant();
			user.UserName = cmd.Email?.ToUpperInvariant();


			var result = await _context.SaveChangesAsync();

			return result;
		}

		public async Task<string> UpdateIsBlockedAsync(UpdateIsBlockedUserAPICommand cmd)
		{
			if (string.IsNullOrEmpty(cmd.UserId)) return "";
			var user = await _context.Users
				.Where(x => x.Id == cmd.UserId)
				.FirstOrDefaultAsync();

			user.IsBlocked = cmd.IsBlocked;
			var result = await _context.SaveChangesAsync();
			return user.Id;
		}

		public async Task<ProfileComponentAPIDto> GetProfileComponentAsync()
		{
			//var user = await _context.Users.Where(x => x.Id == _currentUser.UserId).FirstOrDefaultAsync();
			var user = await _userManager.FindByEmailAsync(_currentUser.Email);
			var role = await _userManager.GetRolesAsync(user);
			var profile = new ProfileComponentAPIDto
			{
				Fullname = user.FirstName + " " + user.LastName,
				Email = user.Email,
				Role = role.FirstOrDefault()

			};
			return profile;
		}
	}
}
