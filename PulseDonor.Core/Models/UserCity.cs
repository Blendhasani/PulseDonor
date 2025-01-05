using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class UserCity
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int CityId { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsPrimary { get; set; }

    public string InsertedFrom { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
