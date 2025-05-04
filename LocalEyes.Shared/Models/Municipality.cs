using System;
using System.Collections.Generic;

namespace LocalEyes.Shared.Models;

public partial class Municipality
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Zipcode { get; set; } = null!;
}
