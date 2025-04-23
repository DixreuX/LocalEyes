using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LocalEyes.Shared.Models;

public partial class Type
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

}
