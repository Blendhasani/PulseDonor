using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class NotificationUser
{
    public int Id { get; set; }

    public int NotificationId { get; set; }

    public string UserId { get; set; } = null!;

    public bool IsRead { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
