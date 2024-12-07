using System;
using System.Collections.Generic;

namespace PulseDonor.Infrastructure.Models;

public partial class SuccessStoryFile
{
    public int Id { get; set; }

    public int SuccessStoryId { get; set; }

    public string FilePath { get; set; } = null!;

    public string InsertedFrom { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string? ModifiedFrom { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual SuccessStory SuccessStory { get; set; } = null!;
}
