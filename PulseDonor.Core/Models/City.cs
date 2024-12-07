using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();

    public virtual ICollection<UserCity> UserCities { get; set; } = new List<UserCity>();
}
