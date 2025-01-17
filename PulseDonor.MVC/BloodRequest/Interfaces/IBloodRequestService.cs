
using PulseDonor.MVC.BloodRequest.Command;
using PulseDonor.MVC.BloodRequest.DTO;

namespace PulseDonor.MVC.BloodRequest.Interfaces
{
	public interface IBloodRequestService
	{
		Task<int> AddAsync(AddBloodeRequestAPICommand cmd);
		Task<List<GetBloodRequestAPIDto>> GetAllAsync();
		Task<GetBloodRequestAPIDto> GetByIdAsync(int id);
		Task<int> EditAsync(EditBloodRequestAPICommand cmd);
		Task<int> DeleteAsync(int id);
	}
}
