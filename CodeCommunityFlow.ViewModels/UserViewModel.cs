using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeCommunityFlow.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int WarningCount { get; set; }
        public string WarningMessage { get; set; }
        public string OldWarningMsg { get; set; }
        public int Score { get; set; }
        public string ImageUser { get; set; }
        public bool isAdmin { get; set; }
        public virtual ReportViewModel Report { get; set; }
 
        public int DeletebyAdminCount { get; set; }
  


    }

}