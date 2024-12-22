using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.City.DTO;

namespace PulseDonor.MVC.City.Interfaces
{
	public interface ICityService
	{
		Task<int> AddCity(AddCityCommand cmd);
		Task<List<CitiesDto>> GetCities();
		Task<EditCityCommand> GetCityById(int id);
		Task<int> EditCity(EditCityCommand cmd);
		Task<int> DeleteCity(int id);
	}
}
