using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeCommunityFlow.DomainModels
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public string QuestionContent { get; set; }
        public DateTime QuestionDateTime { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public int VotesCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        [ForeignKey("UserID")]
        public virtual Users Users { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Categories Categories { get; set; }
        public virtual List<Answers> Answers { get; set; }
      
       
        [InverseProperty("Question")]
        public virtual List<ReportLogs> ReportLogs { get; set; } // Link to log table

        [NotMapped]
        public string ReportIDs => ReportLogs != null
            ? string.Join(",", ReportLogs.Select(r => r.ReportReasonID))
            : string.Empty;



        [InverseProperty("Question")]
        public virtual List<DeleteLogs> DeleteLogs { get; set; } 
    }
}