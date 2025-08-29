using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCommunityFlow.ViewModels
{
    public class EditQuestionViewModel
    {
        public int QuestionID { get; set; }
        [Required]
        public string QuestionName { get; set; }
     
        public string QuestionContent { get; set; }
        public DateTime QuestionDateTime { get; set; }
        public int CategoryID { get; set; }
        public int VotesCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }
        public string Image { get; set; }
        public int UserID { get; set; }
        public string ImgOld { get; set; } // store old upload img 
        public string[] CheckDeleteImage { get; set; }
        public virtual CategoryViewModel Categories { get; set; }
        public string Description { get; set; }


    }
}
