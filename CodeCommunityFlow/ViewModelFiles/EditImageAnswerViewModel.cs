using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCommunityFlow.ViewModels;
namespace CodeCommunityFlow.ViewModelFiles
{
    public class EditImageAnswerViewModel:EditAnswerViewModel
    {
        public List<HttpPostedFileBase> Files { get; set; }
    }
}