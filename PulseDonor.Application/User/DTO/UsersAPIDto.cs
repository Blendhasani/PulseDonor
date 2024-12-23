using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.User.DTO
{
	public class UsersAPIDto
	{
		public string Id { get; set; }
		public string Fullname { get; set; }
		public string Gender { get; set; }
		public int? Age { get; set; }
		public string BloodType { get; set; }
		public string Role { get; set; }
		public string InsertedDate { get; set; }
	}
}
