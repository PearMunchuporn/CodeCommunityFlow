using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.DomainModels
{
    public class DeleteLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeleteLogsID { get; set; }
        public int? QuestionID { get; set; }
        public int? AnswerID { get; set; }
        public int? UserID { get; set; }
        public int? CommentID { get; set; }
        public DateTime DeleteDateTime { get;set;}
        public bool DeleteByAdmin { get; set; }

        [ForeignKey("QuestionID")]
        public virtual Questions Question { get; set; }

        [ForeignKey("AnswerID")]
        public virtual Answers Answer { get; set; }

        [ForeignKey("UserID")]
        public virtual Users UserDeletion { get; set; }
        [ForeignKey("CommentID")]
        public virtual CommentFromAnnouncement CommentFromAnnoucement { get; set; }

        public string DeletionType { get; set; }
    }
}
