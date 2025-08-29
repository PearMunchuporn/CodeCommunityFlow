using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class ContactViewModel
    {
        public int ContactID { get; set; }
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please enter your first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        [Required(ErrorMessage = "Please enter your workemail")]
        public string WorkEmail { get; set; }
        [Required(ErrorMessage = "Please select How do you hear about us")]
        public int HowHearUsID { get; set; }
        [Required(ErrorMessage = "Please enter your company")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Please enter your country")]
        public string Country { get; set; }

        public HowKnowUsViewModel HowKnowUs { get; set; }
    }
}
