using PulseDonor.Application.Authentication.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Authentication.Commands
{
    public class LoginCommand
	{
		[DefaultValue("admin@pulsedonor.com")]

		public string Email { get; set; } = null!;

		[DefaultValue("Admin@123")]

		public string Password { get; set; } = null!;
		//public LoginDto LoginDto { get; set; } = null!;
    }
}
