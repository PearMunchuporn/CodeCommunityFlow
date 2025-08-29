using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCommunityFlow.ViewModels;

namespace CodeCommunityFlow.ViewModelFiles
{
    public class EditImgComment: UpdateCommentViewModel
    {
        public List<HttpPostedFileBase> Files { get; set; }
    }
}