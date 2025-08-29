using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class WarnUserViewModel
    {
        public virtual List<ReportViewModel> ReportReasonTable { get; set; }
        public virtual List<ReportLogsDTO> ReportLogUser { get; set; }
        public virtual List<AnswerViewModel> Answers { get; set; }
        public virtual List<QuestionViewModel> Questions { get; set; }
        public string ImageUser { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int WarningCount { get; set; }
        [Required(ErrorMessage = "Please enter message to warn this user!")]
        public string WarningMessage { get; set; }
        public string OldWarningMsg { get; set; }
        public int ScoreMinus { get; set; }
        public int Score { get; set; }
        public int DeletebyAdminCount { get; set; }
    }
}
