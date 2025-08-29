using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCommunityFlow.ViewModels;
namespace CodeCommunityFlow.ViewModelFiles
{
    public class UpdateUserImage:EditUserDetailsViewModel
    {
        public List<HttpPostedFileBase> FilesImageUser { get; set; }
    }
}