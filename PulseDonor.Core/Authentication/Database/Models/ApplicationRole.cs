using Microsoft.AspNetCore.Identity;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Infrastructure.Authentication.Database.Models
{
	public class ApplicationRole : IdentityRole
	{
		public virtual ICollection<ApplicationUser> Users { get; set; }
	}
}
