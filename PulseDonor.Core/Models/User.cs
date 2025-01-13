using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public int BloodTypeId { get; set; }

    public int GenderId { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int? AccessFailedCount { get; set; }

    public bool IsBlocked { get; set; }

    public bool IsActive { get; set; }

    public bool IsEligible { get; set; }

    public DateTime? LastDonationDate { get; set; }

    public DateTime? InsertedDate { get; set; }

    public virtual ICollection<BloodRequestApplication> BloodRequestApplications { get; set; } = new List<BloodRequestApplication>();

    public virtual ICollection<BloodRequest> BloodRequestAuthors { get; set; } = new List<BloodRequest>();

    public virtual ICollection<BloodRequest> BloodRequestDonors { get; set; } = new List<BloodRequest>();

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<GroupMemberJoinCode> GroupMemberJoinCodes { get; set; } = new List<GroupMemberJoinCode>();

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<NotificationUser> NotificationUsers { get; set; } = new List<NotificationUser>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<UserCity> UserCities { get; set; } = new List<UserCity>();
}
