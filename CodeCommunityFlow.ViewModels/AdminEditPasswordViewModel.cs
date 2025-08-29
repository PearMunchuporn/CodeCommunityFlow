using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
   public class AdminEditPasswordViewModel
    {
      
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Comfirmed Password is required.")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Required]
        public int AdminID { get; set; }
    }
}
