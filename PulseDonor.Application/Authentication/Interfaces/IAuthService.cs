using PulseDonor.Application.Authentication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<string> SignupAsync(SignupCommand command);
        Task<string> LoginAsync(LoginCommand command);
        Task<bool> LogoutAsync();

	}
}
