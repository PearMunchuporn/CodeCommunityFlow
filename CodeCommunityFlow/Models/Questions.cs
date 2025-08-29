using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class Questions
    {
        public Questions()
        {
            Answers = new HashSet<Answers>();
            ReportLogs = new HashSet<ReportLogs>();
        }

        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public DateTime? QuestionDateTime { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? VotesCount { get; set; }
        public int? AnswersCount { get; set; }
        public int? ViewsCount { get; set; }
        public string Image { get; set; }
        public string QuestionContent { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Answers> Answers { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<ReportLogs> ReportLogs { get; set; }
    }
}
