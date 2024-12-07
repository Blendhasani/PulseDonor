using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class GroupMemberJoinCode
{
    public int Id { get; set; }

    public string MemberId { get; set; } = null!;

    public int GroupId { get; set; }

    public string Code { get; set; } = null!;

    public DateTime ExpiracyDate { get; set; }

    public bool IsDeleted { get; set; }

    public string InsertedFrom { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User Member { get; set; } = null!;
}
