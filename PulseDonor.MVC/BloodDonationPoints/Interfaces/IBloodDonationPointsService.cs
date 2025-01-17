
using PulseDonor.MVC.BloodDonationPoint.Commands;
using PulseDonor.MVC.BloodDonationPoint.DTO;

namespace PulseDonor.MVC.BloodDonationPointsController.Interfaces
{
    public interface IBloodDonationPointsService
    {
        Task<int> AddAsync(AddAPIBloodDonationPointCommand cmd);
        Task<List<GetAPIBloodDonationListDto>> GetAllAsync();
        Task<GetAPIBloodDonationListDto> GetCityById(int id);
        Task<int> EditAsync(EditAPIBloodDonationPointCommand cmd);
        Task<int> DeleteAsync(int id);
    }
}
