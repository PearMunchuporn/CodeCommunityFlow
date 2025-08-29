using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeCommunityFlow.DomainModels
{
    [Table("Report")]
    public class Report
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportID { get; set; }

        public string ReportReason { get; set; }
        [InverseProperty("ReportReason")]
        public virtual List<ReportLogs> ReportLogsReason { get; set; }
        public int ScoreMinus { get; set; }
        [InverseProperty("ReasonReportScore")]
        public List<ScoreHistoryLogs> ReasonReportScore { get; set; }
    }
}