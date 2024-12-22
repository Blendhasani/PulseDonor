using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.City.DTO;
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
			string url = "https://localhost:7269/api/City/Add";
			return await _apiClientHelper.PostAsync<AddCityCommand, int>(url, cmd);
		}

		public async Task<List<CitiesDto>> GetCities()
		{
			string url = "https://localhost:7269/api/City/GetList";
			var data = await _apiClientHelper.GetAsync<List<CitiesDto>>(url);

			return data.Select(x => new CitiesDto
			{
				Id = x.Id,
				Name = x.Name
			}).ToList();

		}

		public async Task<EditCityCommand> GetCityById(int id)
		{
			string url = "https://localhost:7269/api/City/Get";
			
			var data = await _apiClientHelper.GetByIdAsync<EditCityCommand>(url, id);

			var singleCity = new EditCityCommand
			{
				Id = data.Id,
				Name = data.Name
			};

			return singleCity;

		}

		public async Task<int> EditCity(EditCityCommand cmd)
		{
			string url = "https://localhost:7269/api/City/Edit";
			return await _apiClientHelper.PutAsync<EditCityCommand, int>(url, cmd);
		}

		public async Task<int> DeleteCity(int id)
		{
			string url = "https://localhost:7269/api/City/Delete?id=" + id;
			return await _apiClientHelper.DeleteAsync<int>(url);
		}
	}
}
