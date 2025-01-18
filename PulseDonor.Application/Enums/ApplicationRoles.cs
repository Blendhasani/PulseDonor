using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Enums
{
	public enum ApplicationRoles
	{
		Admin = 1,
		User = 2
	}

	public static class ApplicationRolesExtensions
	{
		public static string ToString(this ApplicationRoles role)
		{
			return role switch
			{
				ApplicationRoles.Admin => "Admin",
				ApplicationRoles.User => "User",
				_ => "Unknown"
			};
		}
	}

}
