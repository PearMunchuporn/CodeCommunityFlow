using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class Report
    {
        public Report()
        {
            ReportLogs = new HashSet<ReportLogs>();
        }

        public int ReportId { get; set; }
        public string ReportReason { get; set; }

        public virtual ICollection<ReportLogs> ReportLogs { get; set; }
    }
}
