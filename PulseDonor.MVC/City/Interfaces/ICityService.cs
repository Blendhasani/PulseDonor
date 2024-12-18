using PulseDonor.MVC.City.Commands;

namespace PulseDonor.MVC.City.Interfaces
{
	public interface ICityService
	{
		Task<int> AddCity(AddCityCommand cmd);
	}
}
