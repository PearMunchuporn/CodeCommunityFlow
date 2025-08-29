using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCommunityFlow.ViewModels
{
    public class EditAnswerViewModel
    {
        public int AnswerID { get; set; }

        public string AnswerText { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int VotesCount { get; set; }
        public string Image { get; set; }
        public string ImageOld { get; set; } // old image string from DB (same as Image, but kept separately for checking)
        public string[] CheckDeleteImage { get; set; } // for images to delete
        public string Description { get; set; }
        public virtual QuestionViewModel Question { get; set; }

        public virtual List<ReportViewModel> Report { get; set; }
        public virtual UserViewModel User { get; set; }
        public int[] Reported { get; set; }
     
    }
}
