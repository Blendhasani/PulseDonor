using PulseDonor.Application.Hospitals.DTO;
using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.City.DTO;
using PulseDonor.MVC.Helper.Interfaces;
using PulseDonor.MVC.Hospitals.Commands;
using PulseDonor.MVC.Hospitals.DTO;
using PulseDonor.MVC.Hospitals.Interfaces;

namespace PulseDonor.MVC.Hospitals.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IApiClientHelper _apiClientHelper;

        public HospitalService(IApiClientHelper apiClientHelper)
        {
            _apiClientHelper = apiClientHelper;
        }

        public async Task<int> AddAsync(AddHospitalAPICommand cmd)
        {
            string url = "https://localhost:7269/api/Hospital/Add";
            return await _apiClientHelper.PostAsync<AddHospitalAPICommand, int>(url, cmd);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string url = "https://localhost:7269/api/Hospital/Delete?id=" + id;
            return await _apiClientHelper.DeleteAsync<int>(url);
        }

        public async Task<int> EditAsync(EditHospitalCommand cmd)
        {
            string url = "https://localhost:7269/api/Hospital/Edit";
            return await _apiClientHelper.PutAsync<EditHospitalCommand, int>(url, cmd);
        }

        public async Task<List<GetHospitalsAPIDto>> GetAllAsync()
        {
            try
            {
                string url = "https://localhost:7269/api/Hospital";
                var data = await _apiClientHelper.GetAsync<List<GetHospitalsAPIDto>>(url);

                return data.Select(x => new GetHospitalsAPIDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address

                }).ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<GetHospitalAPIDto> GetByIdAsync(int id)
        {
            string url = "https://localhost:7269/api/Hospital/GetById";

            var data = await _apiClientHelper.GetByIdAsync<GetHospitalAPIDto>(url, id);

            var singleCity = new GetHospitalAPIDto
            {
                Id = data.Id,
                Name = data.Name,
                Address = data.Address
            };

            return singleCity;
        }
    }
}
