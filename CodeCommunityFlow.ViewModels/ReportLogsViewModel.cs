using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
   public class ReportLogsViewModel
    {
        public int LogID { get; set; }

        public int ReportReasonID { get; set; }

        public int? ReportedByUserID { get; set; }  //  User who reported 
        public int? QuestionID { get; set; }
        public int? AnswerID { get; set; }
        public string ReportType { get; set; }
        public int? CommentID { get; set; }
        public virtual QuestionViewModel Questions { get; set; }

    
        public virtual AnswerViewModel Answers { get; set; }


        public int? ReportedUserID { get; set; }   // Who was reported


        public virtual UserViewModel Reporter { get; set; }

        public virtual UserViewModel ReportedUser { get; set; }


        public virtual ReportViewModel Report { get; set; }
        public DateTime? ReportedTime { get; set; }
    }
}
