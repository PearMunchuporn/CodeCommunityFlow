using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.ViewModels
{
    public class  newAnswerViewModel
    {
        public string AnswerText { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int VotesCount { get; set; }
        public string Image { get; set; }
        public virtual UserViewModel Users { get; set; }
    }
}
