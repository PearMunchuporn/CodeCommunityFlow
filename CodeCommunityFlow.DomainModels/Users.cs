using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeCommunityFlow.DomainModels
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public string WarningMessage { get; set; }
        public int Score { get; set; }
        public int WarningCount { get; set; }
     
        [InverseProperty("ReportedByUser")]
        public virtual List<ReportLogs> ReportLogsReportedByUser { get; set; }

        [InverseProperty("ReportedUser")]
        public virtual List<ReportLogs> ReportLogsReportedUser { get; set; }
        public string ImageUser { get; set; }
        public int DeletebyAdminCount { get; set; }

        [NotMapped]
        public string ReportIDs =>
            (ReportLogsReportedByUser != null ? string.Join(",", ReportLogsReportedByUser.Select(r => r.ReportReasonID)) : string.Empty) +
            (ReportLogsReportedUser != null ? string.Join(",", ReportLogsReportedUser.Select(r => r.ReportReasonID)) : string.Empty);

        [NotMapped]
        public int ReportCount =>
            (ReportLogsReportedByUser?.Count ?? 0) + (ReportLogsReportedUser?.Count ?? 0);

        [InverseProperty("UserDeletion")]
        public virtual List<DeleteLogs> DeleteLogs { get; set; }

        [InverseProperty("UserScore")]
        public virtual List<ScoreHistoryLogs> ScoreHistoryLogs { get; set; }
      


    }

}