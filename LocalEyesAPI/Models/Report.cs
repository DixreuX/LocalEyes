using System;
using System.Collections.Generic;

namespace LocalEyesAPI.Models;

public partial class Report
{
    public Guid Id { get; set; }

    public Guid TypeId { get; set; }

    public int Priority { get; set; }

    public string Latitude { get; set; } = null!;

    public string Longtitude { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string ModifedBy { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;
}
