using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int NotificationTypeId { get; set; }

    public string SenderId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string MetaData { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public virtual NotificationType NotificationType { get; set; } = null!;

    public virtual ICollection<NotificationUser> NotificationUsers { get; set; } = new List<NotificationUser>();

    public virtual User Sender { get; set; } = null!;
}
