using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.MVC.BloodDonationPoint.DTO
{
    public class GetAPIBloodDonationListDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
