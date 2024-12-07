using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class Group
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string InsertedFrom { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<GroupMemberJoinCode> GroupMemberJoinCodes { get; set; } = new List<GroupMemberJoinCode>();

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
}
