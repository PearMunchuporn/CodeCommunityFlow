using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeCommunityFlow.DomainModels
{
    public class Answers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public int? UserID { get; set; }
        public int QuestionID { get; set; }
   
        public int VotesCount { get; set; }
        public string Image { get; set; }
        public bool? isThankedTo { get; set; }

        [ForeignKey("UserID")]
        public virtual Users Users { get; set; }
        public virtual List<Votes> Votes { get; set; }

        public string Description { get; set; }
       

        [InverseProperty("Answer")]
        public virtual List<ReportLogs> ReportLogs { get; set; }

        [InverseProperty("Answer")]
        public virtual List<DeleteLogs> DeleteLogs { get; set; }

        [ForeignKey("QuestionID")]
        public virtual Questions Questions { get; set; }
        [NotMapped]
        public string ReportIDs => ReportLogs != null && ReportLogs.Any()
            ? string.Join(",", ReportLogs.Select(r => r.ReportReasonID))
            : string.Empty;
    }
}