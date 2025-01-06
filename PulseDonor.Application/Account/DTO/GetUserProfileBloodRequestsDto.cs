using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.DTO
{
	public class GetUserProfileBloodRequestsDto
	{
		public int Id {  get; set; }	
		public string BloodType { get; set; }	
		public decimal Quantity { get; set; }	
		public SingleUserProfileUrgencyType UrgenceType { get; set; }	
		public DateOnly? DonationDate { get; set; }
		public TimeOnly? DonationTime { get; set; }
		public SingleUserProfileHospital? Hospital { get; set; }
		public int NumberOfApplications { get; set; }

	}

	public class SingleUserProfileUrgencyType
	{
		public int Id { get; set; }
		public string Type { get; set; } 
	}

	public class SingleUserProfileHospital
	{
		public int? Id { get; set; }
		public string Name { get; set; }
	}
}
