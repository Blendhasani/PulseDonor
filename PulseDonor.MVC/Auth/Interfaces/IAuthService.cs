using PulseDonor.MVC.Auth.Commands;

namespace PulseDonor.MVC.Auth.Interfaces
{
	public interface IAuthService
	{
		Task<bool> Login(LoginCommand cmd);
	}
}
