using PulseDonor.Application.BloodDonationPoint.Commands;
using PulseDonor.Application.BloodDonationPoint.DTO;
using PulseDonor.Application.Hospitals.Commands;
using PulseDonor.Application.Hospitals.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Hospitals.Interfaces
{
    public interface IHospitalService
    {
        Task<int> AddAsync(AddHospitalCommand cmd);
        Task<List<GetHospitalsDto>> GetAllAsync();
        Task<GetHospitalDto> GetByIdAsync(int id);
        Task<int> EditAsync(EditHospitalCommand cmd);
        Task<int> DeleteAsync(int id);
    }
}
