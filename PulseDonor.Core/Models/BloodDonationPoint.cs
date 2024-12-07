using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class BloodDonationPoint
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool IsDeleted { get; set; }

    public string InsertedFrom { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
