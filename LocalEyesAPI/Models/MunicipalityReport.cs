using LocalEyesAPI.Models;
using LocalEyes.Shared.Models;
using System;
using System.Collections.Generic;

namespace LocalEyesAPI.Models;

public partial class MunicipalityReport
{
    public Guid Id { get; set; }

    public Guid MunicipalityId { get; set; }

    public Guid ReportId { get; set; }

    public virtual Municipality Municipality { get; set; } = null!;

    public virtual Report Report { get; set; } = null!;
}
