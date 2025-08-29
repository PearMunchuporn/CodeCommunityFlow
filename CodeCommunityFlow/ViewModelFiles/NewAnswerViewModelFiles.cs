using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCommunityFlow.ViewModelFiles
{
    public class NewAnswerViewModelFiles: newAnswerViewModel
    {
        public List<HttpPostedFileBase> Files
        {
            get; set;
        }
    }
}