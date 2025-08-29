using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.DomainModels
{
 
        public class ReportLogs
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int LogID { get; set; }

            public int? QuestionID { get; set; }
            public int? AnswerID { get; set; }
            public int? CommentID { get; set; }
            public int? ReportedByUserID { get; set; }
            public int? ReportedUserID { get; set; }
            public int ReportReasonID { get; set; }
            public DateTime ReportedTime { get; set; }
            public string ReportType { get; set; }


            [ForeignKey("QuestionID")]
            public virtual Questions Question { get; set; }

            [ForeignKey("AnswerID")]
            public virtual Answers Answer { get; set; }
            [ForeignKey("CommentID")]
            public virtual CommentFromAnnouncement CommentFromAnnoucement { get; set; } 

            [ForeignKey("ReportReasonID")]
            [InverseProperty("ReportLogsReason")]
            public virtual Report ReportReason { get; set; }

            
            [ForeignKey("ReportedByUserID")]
            [InverseProperty("ReportLogsReportedByUser")]
            public virtual Users ReportedByUser { get; set; }

            [ForeignKey("ReportedUserID")]
            [InverseProperty("ReportLogsReportedUser")]
            public virtual Users ReportedUser { get; set; }
        }


    }


