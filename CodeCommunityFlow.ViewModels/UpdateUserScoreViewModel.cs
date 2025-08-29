using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class UpdateUserScoreViewModel
    {
        public virtual List<ReportViewModel> ReportReasonTable { get; set; }
        public virtual List<ReportLogsDTO> ReportLogUser { get; set; }
        public string ImageUser { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int WarningCount { get; set; }
        public string WarningMessage { get; set; }
        public string OldWarningMsg { get; set; }
        public int ScoreMinus { get; set; }
        [Required(ErrorMessage ="Please enter score that you want to reimburse to this user.")]
        public int Score { get; set; }
    }
}
