using CodeCommunityFlow.CustomFilter;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;


namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    [AuthorizationAdminOnly]
    [Authorization]
    public class ReportManagementController : Controller
    {

        IReportService reportService;
        IAdminLogsActionService adminLogsActionService;
        public ReportManagementController(IReportService irs, IAdminLogsActionService adminLogsActionService)
        {
            this.reportService = irs;
            this.adminLogsActionService = adminLogsActionService;
        }
        // GET: Admin/ReportManagement
        public ActionResult ReportManagement()
        {
            List<ReportViewModel> reports = this.reportService.GetReports().ToList();
            return View(reports);
        }
        public ActionResult CreateReportReason()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateReportReason(ReportViewModel InsertReport)
        {
            if (ModelState.IsValid)
            {
                reportService.InsertReport(InsertReport);
               
                var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
                var AddActionLogs = new AdminActionLogsViewModel
                {
                    ActionByAdminID = adminId,
                    ActionName = "Add Report Reason",
                    ActionDateTime = DateTime.Now,  

                };
                TempData["AddReportReason"] = "Report Reason has been added successfully";
                this.adminLogsActionService.InsertAction(AddActionLogs);
                return RedirectToAction("ReportManagement");
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot add this report reason, please try again.");
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditReportReason(ReportViewModel UpdateReportType)
        {
            if (ModelState.IsValid)
            {
                reportService.EditReport(UpdateReportType);
                var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
                var AddActionLogs = new AdminActionLogsViewModel
                {
                    ActionByAdminID = adminId,
                    ActionName = "Edit Report Reason",
                    ActionDateTime = DateTime.Now,

                };

                this.adminLogsActionService.InsertAction(AddActionLogs);
                TempData["EditReportReason"] = "Report Reason has been edited successfully";
                return RedirectToAction("ReportManagement", "ReportManagement");
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot edit or update this report reason, please try again.");
                return View();
            }
           
        }
        //View Page
        public ActionResult EditReportReason(int id)
        {
            ReportViewModel report = this.reportService.GetReportById(id);
            return View(report);
        }

    }
}