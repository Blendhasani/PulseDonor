using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class Hospital
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();

    public virtual City City { get; set; } = null!;
}
