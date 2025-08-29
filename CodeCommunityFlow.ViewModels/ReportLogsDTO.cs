using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class ReportLogsDTO
    {
        public int LogID { get; set; }
        public DateTime ReportedTime { get; set; }
        public string ReportType { get; set; }

        public UserViewModel Reporter { get; set; }
        public UserViewModel ReportedUser { get; set; }
        public ReportViewModel Report { get; set; }
        public QuestionViewModel Questions { get; set; }
        public AnswerViewModel Answers { get; set; }
    }
}
