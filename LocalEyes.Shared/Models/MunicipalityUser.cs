using LocalEyes.Shared.Models;
using System;
using System.Collections.Generic;

namespace LocalEyes.Shared.Models;

public partial class MunicipalityUser
{
    public Guid Id { get; set; }

    public Guid MunicipalityId { get; set; }

    public Guid UserId { get; set; }

    public virtual Municipality Municipality { get; set; } = null!;
}
