using PulseDonor.Application.City.Commands;
using PulseDonor.Application.City.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.City.Interfaces
{
	public interface ICityAPIService
	{
		Task<int> AddCityAsync(AddCityAPICommand cmd);
		Task<List<CitiesAPIDto>> GetCitiesAsync();
		Task<SingleCityAPIDto> GetCityByIdAsync(int id);
		Task<int> EditCityAsync(EditCityAPICommand cmd);
		Task<int> DeleteCityAsync(int id);

	}
}
