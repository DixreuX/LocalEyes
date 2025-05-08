using LocalEyes.Shared.Models;
using System;
using System.Collections.Generic;

namespace LocalEyes.Shared.Models;

public partial class UserReport
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ReportId { get; set; }
}
