using PulseDonor.MVC.BloodDonationPoint.Commands;
using PulseDonor.MVC.BloodDonationPoint.DTO;
using PulseDonor.MVC.BloodDonationPointsController.Interfaces;
using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.City.DTO;
using PulseDonor.MVC.Helper.Interfaces;

namespace PulseDonor.MVC.BloodDonationPointsController.Services
{
    public class BloodDonationPointsService : IBloodDonationPointsService
    {
        private readonly IApiClientHelper _apiClientHelper;
        public BloodDonationPointsService(IApiClientHelper apiClientHelper)
        {
            _apiClientHelper = apiClientHelper;
        }

        public async Task<int> AddAsync(AddAPIBloodDonationPointCommand cmd)
        {
            string url = "https://localhost:7269/api/blood-donation-points/Add";
            return await _apiClientHelper.PostAsync<AddAPIBloodDonationPointCommand, int>(url, cmd);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string url = "https://localhost:7269/api/blood-donation-points/Delete?id=" + id;
            return await _apiClientHelper.DeleteAsync<int>(url);
        }

        public async Task<int> EditAsync(EditAPIBloodDonationPointCommand cmd)
        {
            string url = "https://localhost:7269/api/blood-donation-points/Edit";
            return await _apiClientHelper.PutAsync<EditAPIBloodDonationPointCommand, int>(url, cmd);
        }

        public async Task<List<GetAPIBloodDonationListDto>> GetAllAsync()
        {
            string url = "https://localhost:7269/api/blood-donation-points/GetAll";
            var data = await _apiClientHelper.GetAsync<List<GetAPIBloodDonationListDto>>(url);

            return data.Select(x => new GetAPIBloodDonationListDto
            {
                Id = x.Id,
                Address = x.Address,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToList();
        }

        public async Task<GetAPIBloodDonationListDto> GetCityById(int id)
        {
            string url = "https://localhost:7269/api/blood-donation-points/Get";

            var data = await _apiClientHelper.GetByIdAsync<GetAPIBloodDonationListDto>(url, id);

            var single = new GetAPIBloodDonationListDto
            {
                Id = data.Id,
                Address = data.Address,
                Latitude = data.Latitude,
                Longitude = data.Longitude,
                StartTime = data.StartTime,
                EndTime = data.EndTime
            };

            return single;
        }
    }
}
