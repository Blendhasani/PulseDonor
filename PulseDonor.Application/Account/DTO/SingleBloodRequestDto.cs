using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.DTO
{
	public class SingleBloodRequestDto
	{
		public int Id { get; set; }
		public SingleBRBloodTypeDto BloodType { get; set; }
		public decimal Quantity { get; set; }
		public SingleBRUrgenceDto UrgenceType { get; set; }
		public SingleBRHospitalDto? Hospital { get; set; }
		public DateOnly? DonationDate { get; set; }
		public TimeOnly? DonationTime { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int? Age { get; set; }
	}

	public class SingleBRBloodTypeDto
	{
		public int BloodTypeId { get; set; }
		public string Type { get; set; }
	}

	public class SingleBRHospitalDto
	{
		public int? HospitalId { get; set; }
		public string Name { get; set; }
	}

	public class SingleBRUrgenceDto
	{
		public int UrgenceTypeId { get; set; }
		public string Type { get; set; }
	}
}
