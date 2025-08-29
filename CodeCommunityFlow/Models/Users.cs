using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class Users
    {
        public Users()
        {
            Answers = new HashSet<Answers>();
            Questions = new HashSet<Questions>();
            ReportLogsReportedByUser = new HashSet<ReportLogs>();
            ReportLogsReportedUser = new HashSet<ReportLogs>();
            Votes = new HashSet<Votes>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool? IsAdmin { get; set; }
        public int? Score { get; set; }
        public int? WarningCount { get; set; }
        public string WarningMessage { get; set; }

        public virtual ICollection<Answers> Answers { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
        public virtual ICollection<ReportLogs> ReportLogsReportedByUser { get; set; }
        public virtual ICollection<ReportLogs> ReportLogsReportedUser { get; set; }
        public virtual ICollection<Votes> Votes { get; set; }
    }
}
