using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.ViewModels;

namespace CodeCommunityFlow.ViewModels
{
    public class DeleteLogsViewModel
    {
        public int DeleteLogsID { get; set; }
        public int? QuestionID { get; set; }
        public int? AnswerID { get; set; }
        public int? UserID { get; set; }
        public int? CommentID { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public bool DeleteByAdmin { get; set; }
        public string DeletionType { get; set; }
        public virtual QuestionViewModel Question { get; set; }

        public virtual CommentFromAnnouncementViewModel Comment { get; set; }
        public virtual AnswerViewModel Answer { get; set; }

        public virtual UserViewModel UserDeletion { get; set; }
    }
}
