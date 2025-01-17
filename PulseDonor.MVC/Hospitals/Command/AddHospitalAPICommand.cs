using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.MVC.Hospitals.Commands
{
    public class AddHospitalAPICommand
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
