using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Authentication.DTO
{
    public class SignupDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
		public int GenderId { get; set; } = 0!;
		public int BloodTypeId { get; set; } = 0!;
		public int CityId { get; set; } = 0!;
	}
}
