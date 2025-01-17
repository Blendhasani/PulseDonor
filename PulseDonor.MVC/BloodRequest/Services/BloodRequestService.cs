using PulseDonor.MVC.BloodRequest.Command;
using PulseDonor.MVC.BloodRequest.DTO;
using PulseDonor.MVC.BloodRequest.Interfaces;
using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.City.DTO;
using PulseDonor.MVC.Helper.Interfaces;

namespace PulseDonor.MVC.BloodRequest.Services
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IApiClientHelper _apiClientHelper;

        public BloodRequestService(IApiClientHelper apiClientHelper)
        {
            _apiClientHelper = apiClientHelper;
        }

        public async Task<int> AddAsync(AddBloodeRequestAPICommand cmd)
        {
            string url = "https://localhost:7269/api/City/Add";
            return await _apiClientHelper.PostAsync<AddBloodeRequestAPICommand, int>(url, cmd);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string url = "https://localhost:7269/api/City/Delete?id=" + id;
            return await _apiClientHelper.DeleteAsync<int>(url);
        }

        public async Task<int> EditAsync(EditBloodRequestAPICommand cmd)
        {
            string url = "https://localhost:7269/api/City/Edit";
            return await _apiClientHelper.PutAsync<EditBloodRequestAPICommand, int>(url, cmd);
        }

        public async Task<List<GetBloodRequestAPIDto>> GetAllAsync()
        {
            string url = "https://localhost:7269/api/City/GetList";
            var data = await _apiClientHelper.GetAsync<List<GetBloodRequestAPIDto>>(url);

            return data.Select(x => new GetBloodRequestAPIDto
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                BloodTypeId = x.BloodTypeId,
                UrgencTypeId = x.UrgencTypeId,
                HospitalId = x.HospitalId,
                DonorId = x.DonorId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                Quantity = x.Quantity,
                PostKey = x.PostKey,
            }).ToList();
        }

        public async Task<GetBloodRequestAPIDto> GetByIdAsync(int id)
        {
            string url = "https://localhost:7269/api/City/Get";

            var data = await _apiClientHelper.GetByIdAsync<GetBloodRequestAPIDto>(url, id);

            var singleCity = new GetBloodRequestAPIDto
            {
                Id = data.Id,
                AuthorId = data.AuthorId,
                BloodTypeId = data.BloodTypeId,
                UrgencTypeId = data.UrgencTypeId,
                HospitalId = data.HospitalId,
                DonorId = data.DonorId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Age = data.Age,
                Quantity = data.Quantity,
                PostKey = data.PostKey,
            };

            return singleCity;
        }
    }
}
