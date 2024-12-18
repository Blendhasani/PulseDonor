using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.City.Interfaces;
using PulseDonor.MVC.Helper.Interfaces;

namespace PulseDonor.MVC.City.Services
{
	public class CityService : ICityService
	{
		private readonly IApiClientHelper _apiClientHelper;

		public CityService(IApiClientHelper apiClientHelper)
		{
			_apiClientHelper = apiClientHelper;
		}

		public async Task<int> AddCity(AddCityCommand cmd)
		{
			string url = "https://localhost:7269/api/CityAPI/addCity";
			return await _apiClientHelper.PostAsync<AddCityCommand, int>(url, cmd);
		}
	}
}
