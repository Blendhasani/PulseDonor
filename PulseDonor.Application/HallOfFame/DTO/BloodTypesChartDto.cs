using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.HallOfFame.DTO
{
	public class BloodTypesChartDto
	{
		public string BloodType { get; set; }	
		public int Count { get; set; }
		public double Percentage { get; set; }
	}
}
