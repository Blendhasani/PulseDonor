using PulseDonor.Application.Authentication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Authentication.Commands
{
    public class LoginCommand
    {
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		//public LoginDto LoginDto { get; set; } = null!;
    }
}
