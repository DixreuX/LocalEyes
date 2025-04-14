using System;
using System.Collections.Generic;

namespace LocalEyesAPI.Models;

public partial class Type
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
