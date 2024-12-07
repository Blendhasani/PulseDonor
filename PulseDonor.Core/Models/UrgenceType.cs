using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class UrgenceType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();
}
