using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class ScoreHistoryViewModel
    {
        public int ScoreHistoryID { get; set; }
        public int? AddAnswer { get; set; }
        public int? UpdateAnswer { get; set; }
        public int? WarnedByAdmin { get; set; }
        public int? DeletedByAdmin { get; set; }
        public int? GotVoteUp { get; set; }
        public int? GotVoteDown { get; set; }
        public int? OldScore { get; set; }
        public int? UserID { get; set; }
        public int? GotReport { get; set; }
        public int? ReportReasonID { get; set; }
        public int? UpdateScoreByAdmin { get; set; }
        public int? GotThankTo { get; set; }
        public virtual UserViewModel UserScore { get; set; }
        public virtual ReportViewModel ReasonReportScore { get; set; }
    }
}
