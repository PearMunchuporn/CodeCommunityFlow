using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    public class ActionLogsController : Controller
    {
        // GET: Admin/ActionLogs
        IAdminLogsActionService adminLogsActionService;
        public ActionLogsController(IAdminLogsActionService adminLogsActionService)
        {
            this.adminLogsActionService =adminLogsActionService;
        }
        public ActionResult ActionLogs()
        {

            var adminActionLogs = this.adminLogsActionService.GetActions().ToList();

            return View(adminActionLogs);
        }
    }
}