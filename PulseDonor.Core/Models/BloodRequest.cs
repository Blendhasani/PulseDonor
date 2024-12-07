using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class BloodRequest
{
    public int Id { get; set; }

    public string AuthorId { get; set; } = null!;

    public int BloodTypeId { get; set; }

    public int UrgenceTypeId { get; set; }

    public int? HospitalId { get; set; }

    public string? DonorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public decimal Quantity { get; set; }

    public string PostKey { get; set; } = null!;

    public DateOnly? DonationDate { get; set; }

    public TimeOnly? DonationTime { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime InsertedDate { get; set; }

    public string InsertedFrom { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<BloodRequestApplication> BloodRequestApplications { get; set; } = new List<BloodRequestApplication>();

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual User? Donor { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual UrgenceType UrgenceType { get; set; } = null!;
}
