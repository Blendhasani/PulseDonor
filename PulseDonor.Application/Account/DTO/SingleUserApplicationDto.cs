using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.DTO
{
    public class SingleUserApplicationDto
    {
        public int Id { get; set; }
		public string PostKey { get; set; }
        public decimal Quantity {get; set;}
        public SingleApplicationUrgenceType Urgence {get; set;}
        public SingleApplicationBloodType BloodType {get; set;}
        public DateOnly? DonationDate {get; set;}
        public TimeOnly? DonationTime {get; set;}
        public bool? IsAccepted {get; set;}


	}

    public class SingleApplicationUrgenceType
    {
        public int UrgenceTypeId { get; set; }
        public string UrgenceType { get; set; }
    }

	public class SingleApplicationBloodType
	{
		public int BloodTypeId { get; set; }
		public string BloodType { get; set; }
	}

}
