using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.MVC.BloodRequest.DTO
{
    public class GetBloodRequestAPIDto
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public int BloodTypeId { get; set; }
        public int UrgencTypeId { get; set; }
        public int? HospitalId { get; set; }
        public string DonorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public decimal Quantity { get; set; }
        public string PostKey { get; set; }
        public DateOnly? DonationDate { get; set; }
        public TimeOnly? DonationTime { get; set; }
    }
}
