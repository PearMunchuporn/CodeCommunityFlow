using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CodeCommunityFlow.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public string QuestionContent { get; set; }
        public DateTime? QuestionDateTime { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please select category of question")]
        public int CategoryID { get; set; }
        public int VotesCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }
        public string Image { get; set; }

        public int CurrentUserVoteType { get; set; }
        public UserViewModel Users { get; set; }

        public CategoryViewModel Categories { get; set; }
        public virtual List<AnswerViewModel> Answers { get; set; }
        public virtual List<ReportLogsViewModel> ReportReason { get;set;}
        public string Description { get; set; }
        
    }
}