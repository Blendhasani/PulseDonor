using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.Helper.Interfaces;
using PulseDonor.MVC.User.Commands;
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

		public async Task<string> AddUser(AddUserCommand cmd)
		{
			string url = "https://localhost:7269/api/User/Add";
			return await _apiClientHelper.PostAsync<AddUserCommand, string>(url, cmd);
		}

		public async Task<List<UsersDto>> GetUsers()
		{
			string url = "https://localhost:7269/api/User/GetList";
			var data = await _apiClientHelper.GetAsync<List<UsersDto>>(url);

			return data.Select(x => new UsersDto
			{
				Id = x.Id,
				Fullname = x.Fullname,
				Email = x.Email,
				Gender = x.Gender,
				Age = x.Age,
				BloodType = x.BloodType,
				Role = x.Role,
				IsBlocked = x.IsBlocked,
				InsertedDate = x.InsertedDate
			}).ToList();

		}

		public async Task<EditUserCommand> GetUserById(string id)
		{
			string url = "https://localhost:7269/api/User/Get";

			var data = await _apiClientHelper.GetByStringIdAsync<EditUserCommand>(url, id);

			var singleCity = new EditUserCommand
			{
				Id = data.Id,
				FirstName = data.FirstName,
				LastName = data.LastName,
				Email = data.Email
			};

			return singleCity;

		}

		public async Task<string> EditUser(EditUserCommand cmd)
		{
			string url = "https://localhost:7269/api/User/Edit";
			return await _apiClientHelper.PutAsync<EditUserCommand, string>(url, cmd);
		}

		public async Task<string> UpdateIsBlocked(UpdateIsBlockedUserCommand cmd)
		{
			string url = "https://localhost:7269/api/User/UpdateIsBlocked";
			return await _apiClientHelper.PutAsync<UpdateIsBlockedUserCommand, string>(url, cmd);

		}

		public async Task<ProfileComponentDto> GetProfileComponent()
		{
			string url = "https://localhost:7269/api/User/ProfileComponent";
			var data = await _apiClientHelper.GetAsync<ProfileComponentDto>(url);

			return new ProfileComponentDto
			{
				Fullname = data.Fullname,
				Email = data.Email,
				Role = data.Role
			};
		}
	}
}
