
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class ReportViewModel
    {
        public int ReportID { get; set; }
        [Required(ErrorMessage="Please enter report reason.")]
        [Display(Name = "Report Reason")]
        public string ReportReason { get; set; }
        [Required(ErrorMessage = "Please defind score for this repoert reason")]
        [Display(Name= "Score Effect")]
        public int ScoreMinus { get; set; }
     
    }
}
