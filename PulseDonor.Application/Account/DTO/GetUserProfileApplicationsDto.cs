using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.DTO
{
	public class GetUserProfileApplicationsDto
	{
		public int Id { get; set; }	
		public int BloodRequestId { get; set; }
		public string PostKey { get; set; }
        public decimal Quantity { get; set; }
        public SingleApplicationUrgenceDto Urgence { get; set; }
		public SingleApplicationBloodTypeDto BloodType { get; set; }	
		public DateOnly? DonationDate { get; set; }
		public TimeOnly? DonationTime { get; set; }
		public bool? IsAccepted { get; set;}	
	}

	public class SingleApplicationUrgenceDto
	{
		public int UrgenceTypeId { get; set; }	
		public string UrgenceType { get; set; }

	}

	public class SingleApplicationBloodTypeDto
	{
		public int BloodTypeId { get; set; }
		public string BloodType { get; set; }

	}
}
