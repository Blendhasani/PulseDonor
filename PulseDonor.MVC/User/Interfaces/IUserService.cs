
using PulseDonor.MVC.User.DTO;

namespace PulseDonor.MVC.User.Interfaces
{
	public interface IUserService
	{
		Task<List<UsersDto>> GetUsers();

	}
}
