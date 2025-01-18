using PulseDonor.Application.BloodRequest.Command;
using PulseDonor.Application.BloodRequest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodRequest.Interfaces
{
    public interface IBloodRequestAPIService
    {
        Task<int> AddAsync(AddBloodeRequestCommand cmd);
        Task<List<GetBloodRequestDto>> GetAllAsync();
        Task<GetBloodRequestDto> GetByIdAsync(int id);
        Task<int> EditAsync(EditBloodRequestCommand cmd);
        Task<int> DeleteAsync(int id);
        Task<int> ApplyForRequestAsync(int id);
    }
}
