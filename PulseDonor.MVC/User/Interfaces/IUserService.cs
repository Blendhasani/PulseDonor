
using PulseDonor.MVC.User.Commands;
using PulseDonor.MVC.User.DTO;

namespace PulseDonor.MVC.User.Interfaces
{
	public interface IUserService
	{
		Task<List<UsersDto>> GetUsers();
		Task<string> AddUser(AddUserCommand cmd);
		Task<EditUserCommand> GetUserById(string id);
		Task<string> EditUser(EditUserCommand cmd);


	}
}
