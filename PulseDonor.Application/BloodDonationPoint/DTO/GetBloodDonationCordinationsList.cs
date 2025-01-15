using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodDonationPoint.DTO
{
    public class GetBloodDonationCordinationsList
    {
        public List<Cordinates> Cordinates { get; set; }
    }

    public class Cordinates
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
