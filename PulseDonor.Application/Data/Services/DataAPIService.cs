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
			var data = await _context.BloodTypes.Select(x => new DropdownDataDto
			{
				Value = x.Id.ToString(),
				Text = x.Type
			}).ToListAsync();
			return data;
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
