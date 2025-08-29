using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class HowKnowUsViewModel
    {
        [Required(ErrorMessage = "Please select How do you hear about us")]
        public int HowHearUsID { get; set; }
        public string Know { get; set; }
    }
}
