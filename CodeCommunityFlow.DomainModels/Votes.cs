using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.DomainModels
{
    public class Votes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteID { get; set; }
        public int? UserID { get; set; }
        public int? AnswerID { get; set; }
        public int? QuestionID { get; set; }
        public int VoteValue { get; set; }
     
        public int? CommentID { get; set; }
        public int? AnnounceID { get; set; }
        public int? AdminID { get; set; }
    }
}