using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class AdminActionLogsViewModel
    {
        public int ActionID { get; set; }
        public string ActionName { get; set; }

        public int ActionByAdminID { get; set; }

        public int? UserID { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string UserName { get; set; }
      
        public AdminUserViewModels Admin { get; set; }
      
        public UserViewModel User { get; set; }
    }
}
