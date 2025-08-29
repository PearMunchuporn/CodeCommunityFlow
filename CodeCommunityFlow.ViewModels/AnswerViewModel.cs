using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class AnswerViewModel
    {
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int VotesCount { get; set; }
        public string Image { get; set; }
        public bool? isThankedTo { get; set; }
        public string Description { get; set; }
        public virtual UserViewModel Users { get; set; }
        public virtual List<VoteViewModel> Votes { get; set; }
        public int CurrentUserVoteType { get; set; }
        public virtual List<ReportViewModel> Report { get; set; }
 
        
    }
}
