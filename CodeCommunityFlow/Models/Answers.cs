using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class Answers
    {
        public Answers()
        {
           
            ReportLogs = new HashSet<ReportLogs>();
            Votes = new HashSet<Votes>();
        }

        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public DateTime? AnswerDateTime { get; set; }
        public int? UserId { get; set; }
        public int? QuestionId { get; set; }
        public int? VotesCount { get; set; }
        public string Image { get; set; }
    

        [ForeignKey("QuestionId")]
        public virtual Questions Question { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

      

        [InverseProperty("Answer")]
        public virtual ICollection<ReportLogs> ReportLogs { get; set; }

        public virtual ICollection<Votes> Votes { get; set; }
    }

}
