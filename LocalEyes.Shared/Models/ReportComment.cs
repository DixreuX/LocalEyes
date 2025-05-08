using System;
using System.Collections.Generic;
using LocalEyes.Shared.Models;

namespace LocalEyes.Shared.Models;

public partial class ReportComment
{
    public Guid Id { get; set; }

    public Guid ReportId { get; set; }

    public Guid CommentId { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual Report Report { get; set; } = null!;
}
