using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.ViewModels
{
    public class newQuestionViewModel
    {
        [Required]
        public string QuestionName { get; set; }
     

        public string QuestionContent { get; set; }
        public DateTime? QuestionDateTime { get; set; }
        public int UserID { get; set; }
      
        public int CategoryID { get; set; }
        public int VotesCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }
        public string Image { get; set; }
        public CategoryViewModel Categories { get; set; }
   

    }
}
