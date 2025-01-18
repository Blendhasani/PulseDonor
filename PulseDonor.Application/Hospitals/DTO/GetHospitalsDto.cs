﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Hospitals.DTO
{
    public class GetHospitalsDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
