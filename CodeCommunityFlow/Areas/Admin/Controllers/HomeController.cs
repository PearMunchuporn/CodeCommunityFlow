using CodeCommunityFlow.CustomFilter;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    [AuthorizationAdminOnly]
    [Authorization]
    public class HomeController : Controller
    {
        // GET: Admin/Home

        IQuestionService questionService;
        IAdminAnnounceService adminAnnounceService;
 

        public HomeController(IQuestionService iqs , IAdminAnnounceService adminAnnounceService)
        {
            this.questionService = iqs;
            this.adminAnnounceService = adminAnnounceService;
       
        }
      
        public ActionResult AdminIndex()
        {
            if (Session["CurrentAdminID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            List<QuestionViewModel> Questions = this.questionService.GetQuestions().OrderByDescending(q => q.QuestionDateTime).Take(10).ToList();
            List<AdminAnnouncementViewModels> announcementByAdmin = this.adminAnnounceService.GetAnnoucement().ToList();
            var model = new HomeIndexViewModel
            {
                Questions = Questions,
                AdminAnnouncements = announcementByAdmin
            };

            return View(model);
            
        }
    }
}