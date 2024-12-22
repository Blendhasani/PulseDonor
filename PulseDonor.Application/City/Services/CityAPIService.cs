using PulseDonor.Application.City.Commands;
using PulseDonor.Application.City.DTO;
using PulseDonor.Application.City.Interfaces;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PulseDonor.Application.City.Services
{
	public class CityAPIService : ICityAPIService
	{
		private readonly DevPulsedonorContext _context;
		public CityAPIService(DevPulsedonorContext context)
		{
			_context = context;
		}
		public async Task<int> AddCityAsync(AddCityAPICommand cmd)
		{
			var newCity = new PulseDonor.Infrastructure.Models.City
			{
				Name = cmd.Name
			};

			await _context.Cities.AddAsync(newCity);
			await _context.SaveChangesAsync();

			return newCity.Id;
		}

		public async Task<List<CitiesAPIDto>> GetCitiesAsync()
		{
			var citiesQuery = _context.Cities.AsQueryable();

			return citiesQuery.Select(x => new CitiesAPIDto
			{
				Id = x.Id,
				Name = x.Name
			}).ToList();

		}

		public async Task<SingleCityAPIDto> GetCityByIdAsync(int id)
		{
			var cityQuery = _context.Cities.Where(x => x.Id == id).AsQueryable();

			var city = cityQuery.Select(x => new SingleCityAPIDto
			{
				Id = x.Id,
				Name = x.Name
			}).FirstOrDefault();

			return city;
		}

		public async Task<int> EditCityAsync(EditCityAPICommand cmd)
		{
			if(cmd.Id == 0) return 0;
			var city = await _context.Cities
				.Where(x=> x.Id == cmd.Id)
				.FirstOrDefaultAsync();

			city.Name = cmd.Name;
			
			await _context.SaveChangesAsync();

			return city.Id;
		}

		public async Task<int> DeleteCityAsync(int id)
		{
			if (id == 0) return 0;
			var city = await _context.Cities
				.Where(x => x.Id == id)
				.FirstOrDefaultAsync();

		    _context.Cities.Remove(city);
			await _context.SaveChangesAsync();

			return 1;
		}
	}
}
