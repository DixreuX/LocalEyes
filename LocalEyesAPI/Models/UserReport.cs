using LocalEyesAPI.Models;
using LocalEyes.Shared.Models;
using LocalEyesAPI.Data;
using System;
using System.Collections.Generic;

namespace LocalEyesAPI.Models;

public partial class UserReport
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ReportId { get; set; }

    public virtual Report Report { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
