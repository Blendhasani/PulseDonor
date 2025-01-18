using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.DTO
{
	public class SingleBloodRequestApplicationDto
	{
		public int Id { get;set; }
		public int BloodRequestId { get; set; }	
		public string Fullname { get; set; }	
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }	
		public string BloodType { get; set; }
		public bool? IsAccepted { get; set; }	
		public bool CanConfirm { get; set; }
	}
}
