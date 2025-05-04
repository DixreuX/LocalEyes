using System;
using System.Collections.Generic;

namespace LocalEyes.Shared.Models;

public partial class Comment
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Comment1 { get; set; } = null!;
}
