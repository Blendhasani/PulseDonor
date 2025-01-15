using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.HallOfFame.DTO
{
	public class TopOneHundredDonorsDto
	{
		public int MyPosition { get; set; }
		public List<OneHundredDonorsDto> Donors { get; set; }	

	}

	public class OneHundredDonorsDto
	{
		public string FullName { get; set; }
		public int Count { get; set; }
	}
}
