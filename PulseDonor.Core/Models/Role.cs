﻿using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
}
