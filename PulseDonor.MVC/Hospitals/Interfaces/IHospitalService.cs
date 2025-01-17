
using PulseDonor.Application.Hospitals.DTO;
using PulseDonor.MVC.Hospitals.Commands;
using PulseDonor.MVC.Hospitals.DTO;

namespace PulseDonor.MVC.Hospitals.Interfaces
{
    public interface IHospitalService
    {
        Task<int> AddAsync(AddHospitalAPICommand cmd);
        Task<List<GetHospitalsAPIDto>> GetAllAsync();
        Task<GetHospitalAPIDto> GetByIdAsync(int id);
        Task<int> EditAsync(EditHospitalCommand cmd);
        Task<int> DeleteAsync(int id);
    }
}
