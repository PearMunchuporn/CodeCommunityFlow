using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCommunityFlow.ViewModels;
namespace CodeCommunityFlow.ViewModelFiles
{
    public class AddImgForAnnouncementByAdmin: AnnouncementCreateViewModel
    {
        public List<HttpPostedFileBase> Files { get; set; }
    }
}