using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
   public class AdminUserViewModels:LoginViewModel
    {
        public int AdminID { get; set; }
      
        public string AdminName { get; set; }
        public bool isAdmin { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
  

    }
}
