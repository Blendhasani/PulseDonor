using PulseDonor.Application.HallOfFame.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.HallOfFame.Interfaces
{
	public interface IHallOfFameService
	{
		Task<List<TopThreeDonorsDto>> GetTopThreeDonorsAsync();
		Task<List<TopOneHundredDonorsDto>> GetTopOneHundredDonorsAsync();
		Task<List<BloodTypesChartDto>> GetBloodTypesChartAsync();
	}
}
