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


namespace PulseDonor.Application.User.Services
{
	public class UserAPIService : IUserAPIService
	{
		private readonly DevPulsedonorContext _context;
		private readonly UserManager<PulseDonor.Infrastructure.Models.User> _userManager;


		public UserAPIService(DevPulsedonorContext context, UserManager<PulseDonor.Infrastructure.Models.User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<string> AddUserAsync(AddUserAPICommand command)
		{
			var user = new PulseDonor.Infrastructure.Models.User()
			{
				FirstName = command.FirstName,
				LastName = command.LastName,
				Email = command.Email,
				ImagePath = null,
				UserName = "bldmc",
				EmailConfirmed = true,
				InsertedDate = DateTime.UtcNow
			};

			var result = await _userManager.CreateAsync(user, "Inno@2024");
			if (!result.Succeeded)
			{
				return "Request Failed";
			}

			await _userManager.AddToRoleAsync(user, "Admin");
			return user.Id;
		}

		public async Task<List<UsersAPIDto>> GetUsersAsync()
		{
			var usersQuery = _context.Users
				.AsQueryable();
			var users = await usersQuery
	   .Select(x => new
	   {
		   x.Id,
		   Fullname = x.FirstName + " " + x.LastName,
		   Gender = x.Gender.Type,
		   Birthdate = x.Birthdate,
		   BloodType = x.BloodType.Type,
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
					Gender = user.Gender,
					Age = user.Birthdate != null ? DateTime.Now.Year - user.Birthdate.Value.Year : null,
					BloodType = user.BloodType,
					Role = roles.FirstOrDefault() ?? "No Role",
					InsertedDate = user.InsertedDate
				});
			}

			return usersWithRoles;
		}
	}
}
