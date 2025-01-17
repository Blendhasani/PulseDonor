using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodRequest.DTO
{
    public class GetBloodRequestDto
    {
        public int Id { get; set; }
        public SingleBloodRequestAuthor Author { get; set; }
        public SingleBloodRequestBloodType BloodType { get; set; }
        public SingleBloodRequestUrgence UrgenceType { get; set; }
        public SingleBloodRequestHospital? Hospital { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public decimal Quantity { get; set; }
        public string PostKey { get; set; }
        public DateOnly? DonationDate { get; set; }
        public TimeOnly? DonationTime { get; set; }
    }

	public class SingleBloodRequestAuthor
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
	public class SingleBloodRequestUrgence {
        public int Id  { get; set; }
        public string Type  { get; set; }
    }

	public class SingleBloodRequestBloodType
	{
		public int Id { get; set; }
		public string Type { get; set; }
	}

	public class SingleBloodRequestHospital
	{
		public int? Id { get; set; }
		public string Name { get; set; }
	}

}
