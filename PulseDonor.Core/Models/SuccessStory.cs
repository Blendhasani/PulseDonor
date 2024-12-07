using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class SuccessStory
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string InsertedFrom { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<SuccessStoryFile> SuccessStoryFiles { get; set; } = new List<SuccessStoryFile>();
}
