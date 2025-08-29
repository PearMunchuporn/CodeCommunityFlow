using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.DomainModels
{
    public class ScoreHistoryLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScoreHistoryID { get; set; }
        public int? AddAnswer { get; set; }
        public int? UpdateAnswer { get; set; }
        public int? WarnedByAdmin { get; set; }
        public int? DeletedByAdmin { get; set; }
        public int? GotVoteUp { get; set; }
        public int? GotVoteDown { get; set; }
        public int OldScore { get; set; }
        public int? UserID { get; set; }
        public int? GotReport { get; set; }

        [ForeignKey("UserID")]
        public virtual Users UserScore { get; set; }
        public int? ReportReasonID { get; set; }
        [ForeignKey("ReportReasonID")]
        public virtual Report ReasonReportScore { get;set;}
     
        public int? UpdateScoreByAdmin { get; set; }

        public int? GotThankTo { get; set; }


    }
}
