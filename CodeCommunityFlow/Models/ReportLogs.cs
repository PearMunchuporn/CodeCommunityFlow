using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class ReportLogs
    {
        public int LogId { get; set; }
        public int? ReportReasonId { get; set; }
        public int? ReportedByUserId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }

        public int? ReportedUserId { get; set; }
        public DateTime? ReportedTime { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answers Answer { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Questions Question { get; set; }



        [ForeignKey("ReportReasonId")]
        public virtual Report ReportReason { get; set; }

        [ForeignKey("ReportedByUserId")]
        [InverseProperty("ReportLogsReportedByUser")]
        public virtual Users ReportedByUser { get; set; }

        [ForeignKey("ReportedUserId")]
        [InverseProperty("ReportLogsReportedUser")]
        public virtual Users ReportedUser { get; set; }

    }
}
