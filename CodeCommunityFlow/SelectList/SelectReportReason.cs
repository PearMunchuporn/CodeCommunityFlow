using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeCommunityFlow.SelectList
{
    public class SelectReportReason:QuestionViewModel
    {
        public IEnumerable<SelectListItem> ReportReasonSelectList { get; set; }

    }
}