using PulseDonor.Application.HallOfFame.Commands;
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
		Task<int> CreateGroupAsync(AddGroupCommand cmd);
		Task<string> CreateJoinCodeAsync(int groupId);
		Task<string> JoinGroupAsync(int groupId, JoinGroupCommand cmd);

	}
}
