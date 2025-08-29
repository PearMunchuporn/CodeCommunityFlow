using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    public class ReportLogsController : Controller
    {
        // GET: Admin/ReportLogs

        IReportLogsService reportLogsService;

        public ReportLogsController(IReportLogsService irls)
        {
            this.reportLogsService = irls;
        }

        public ActionResult ReportLogs()
        {
            List<ReportLogsDTO> reportlogs = this.reportLogsService.GetReportLogs().ToList();
          

            return View(reportlogs);
        }
    }
}