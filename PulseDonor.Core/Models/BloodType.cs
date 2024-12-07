using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class BloodType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
