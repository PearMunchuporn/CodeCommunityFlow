using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ViewModels;
namespace CodeCommunityFlow.SelectList
{
    public class SelectReportComment:AdminAnnouncementViewModels
    {
        public IEnumerable<SelectListItem> ReportReasonSelectList { get; set; }
    }

}