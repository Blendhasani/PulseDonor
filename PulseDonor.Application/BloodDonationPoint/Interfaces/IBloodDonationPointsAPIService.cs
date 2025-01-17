using PulseDonor.Application.BloodDonationPoint.Commands;
using PulseDonor.Application.BloodDonationPoint.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodDonationPoint.Interfaces
{
    public interface IBloodDonationPointsAPIService
    {
        Task<int> AddAsync(AddBloodDonationPointCommand cmd);
        Task<List<GetBloodDonationListDto>> GetAllAsync();
        Task<GetBloodDonationListDto> GetByIdAsync(int id);
        Task<int> EditAsync(EditBloodDonationPointCommand cmd);
        Task<int> DeleteAsync(int id);
    }
}
