using PulseDonor.MVC.Helper.Interfaces;
using PulseDonor.MVC.User.DTO;
using PulseDonor.MVC.User.Interfaces;

namespace PulseDonor.MVC.User.Services
{
	public class UserService : IUserService
	{
		private readonly IApiClientHelper _apiClientHelper;

		public UserService(IApiClientHelper apiClientHelper)
		{
			_apiClientHelper = apiClientHelper;
		}

		public async Task<List<UsersDto>> GetUsers()
		{
			string url = "https://localhost:7269/api/User/GetList";
			var data = await _apiClientHelper.GetAsync<List<UsersDto>>(url);

			return data.Select(x => new UsersDto
			{
				Id = x.Id,
				Fullname = x.Fullname,
				Gender = x.Gender,
				Age = x.Age,
				BloodType = x.BloodType,
				Role = x.Role,
				InsertedDate = x.InsertedDate
			}).ToList();

		}
	}
}
