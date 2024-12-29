using PulseDonor.MVC.Auth.Commands;
using PulseDonor.MVC.Auth.Interfaces;
using PulseDonor.MVC.Helper.Interfaces;

namespace PulseDonor.MVC.Auth.Services
{
	public class AuthService : IAuthService
	{
		private readonly IApiClientHelper _apiClientHelper;
        public AuthService(IApiClientHelper apiClientHelper)
        {
			_apiClientHelper = apiClientHelper;
		}

		public async Task<bool> Login(LoginCommand cmd)
		{
			string url = "https://localhost:7269/api/Auth/login";
			var token = await _apiClientHelper.LoginAsync<LoginCommand, string>(url, cmd);

			if (!string.IsNullOrEmpty(token))
			{
				return true; // Token is already stored in the session by LoginAsync
			}

			return false;
		}
	}
}
