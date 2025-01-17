﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.BloodRequest.Command
{
    public class EditBloodRequestCommand
    {
        public int Id { get; set; }
        public int BloodTypeId { get; set; }
        public int UrgenceTypeId { get; set; }
        public int? HospitalId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public decimal Quantity { get; set; }
        public DateOnly? DonationDate { get; set; }
        public TimeOnly? DonationTime { get; set; }
    }
}
