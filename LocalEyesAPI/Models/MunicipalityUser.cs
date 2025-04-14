using LocalEyesAPI.Data;
using System;
using System.Collections.Generic;

namespace LocalEyesAPI.Models;

public partial class MunicipalityUser
{
    public Guid Id { get; set; }

    public Guid MunicipalityId { get; set; }

    public Guid UserId { get; set; }

    public virtual Municipality Municipality { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
