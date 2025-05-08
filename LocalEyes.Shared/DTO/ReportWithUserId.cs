using LocalEyes.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalEyes.Shared.DTO
{
    public class ReportWithUserId
    {
        public Report Report { get; set; } = null!;
        public UserReport UserReport { get; set; } = null!;

    }
}
