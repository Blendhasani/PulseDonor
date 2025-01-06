using PulseDonor.Application.Account.Commands;
using PulseDonor.Application.Account.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.Interfaces
{
	public interface IAccountService
	{
		Task<SingleAccountOverviewDto> GetAccountOverviewAsync();
		Task<string> EditAccountOverviewAsync(EditAccountOverviewCommand cmd);
		Task<List<GetUserProfileBloodRequestsDto>> GetMyBloodRequestsAsync();
		Task<int> CreateBloodRequestPostAsync(AddBloodRequestCommand cmd);
		Task<SingleBloodRequestDto> GetBloodRequestByIdAsync(int id);
		Task<int> EditBloodRequestPostAsync(int id, EditBloodRequestCommand cmd);
		Task<string> DeleteBloodRequestPostAsync(int id);




	}
}
