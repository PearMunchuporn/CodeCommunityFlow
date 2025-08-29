using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeCommunityFlow.ViewModels
{
   public  class HomeIndexViewModel
    {

        public List<QuestionViewModel> Questions { get; set; }
        public List<AdminAnnouncementViewModels> AdminAnnouncements { get; set; }
    }
}
