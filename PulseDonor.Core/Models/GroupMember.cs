using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class GroupMember
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public string MemberId { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string InsertedFrom { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User Member { get; set; } = null!;
}
