using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.DTO
{
	public class SingleAccountOverviewDto
	{
		public string FirstName { get; set; }	
		public string LastName { get; set; }
		public SingleBloodTypeDto BloodType { get; set; }
		public SinglePrimaryCityDto PrimaryCity { get; set; }
		public List<SingleSecondaryCityDto> SecondaryCities { get; set; }
		public bool Status { get; set; } //we use this status to tell if that person wants to donate soon or not
	}

	public class SingleBloodTypeDto
	{
		public int BloodTypeId { get; set; }	
		public string Type { get;set; }
	}

	public class SinglePrimaryCityDto
	{
		public int PrimaryCityId { get; set; }
		public string Name { get; set; }
	}

	public class SingleSecondaryCityDto
	{
		public int SecondaryCityId { get; set; }
		public string Name { get; set; }

	}
}
