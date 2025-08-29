using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCommunityFlow.ViewModels;

namespace CodeCommunityFlow.ViewModelFiles
{
    public class UpdateImgAnnouncementByAdmin: AnnouncementUpdateViewModel
    {
        public List<HttpPostedFileBase> Files { get; set; }
    }
}