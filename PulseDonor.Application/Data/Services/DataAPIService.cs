using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.Data.DTO;
using PulseDonor.Application.Data.Interfaces;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Data.Services
{
	public class DataAPIService : IDataAPIService
	{
        private readonly DevPulsedonorContext _context;
		public DataAPIService(DevPulsedonorContext context)
		{
			_context = context;
		}

		public async Task<List<DropdownDataDto>> GetBloodTypes()
		{
			// Get all enum values
			var data = Enum.GetValues(typeof(Enums.BloodType))
				.Cast<Enums.BloodType>()
				.Select(bloodType => new DropdownDataDto
				{
					Value = ((int)bloodType).ToString(), // Enum integer value
					Text = bloodType.ToString().Replace('_', ' ') // Enum name with underscores replaced by spaces
				})
				.ToList();

			return await Task.FromResult(data); // Simulate asynchronous behavior
		}


		public async Task<List<DropdownDataDto>> GetUsers()
		{
			var data = await _context.Users.Select(x => new DropdownDataDto
			{
				Value = x.Id.ToString(),
				Text = x.UserName
			}).ToListAsync();
			return data;
		}


		public async Task<List<DropdownDataDto>> GetCities()
		{
			var data = await _context.Cities.Select(x => new DropdownDataDto
			{
				Value = x.Id.ToString(),
				Text = x.Name
			}).ToListAsync();
			return data;
		}
	}
}
