using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.CustomFilter;

namespace CodeCommunityFlow.Controllers
{ 
  
    public class HomeController : Controller
    {
        // GET: Home
  
        IQuestionService questionService;
        ICategoryService categoryService;
        IUserService userService;
        IAdminAnnounceService adminAnnounceService;

        public HomeController(ICategoryService ics, IQuestionService iqs, IUserService ius, IAdminAnnounceService adminAnnounceService)
        {
            this.questionService = iqs;
            this.categoryService = ics;
            this.userService = ius;
            this.adminAnnounceService = adminAnnounceService;
        }
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {

            int currentUserId = Convert.ToInt32(Session["CurrentUserID"]);

            var announcementByAdmin = adminAnnounceService.GetAnnoucement().ToList();

            var allQuestions = questionService.GetQuestions().OrderByDescending(q => q.QuestionDateTime).ToList();

            var userQuestions = allQuestions
                .Where(q => q.UserID == currentUserId)
                .Take(10)
                .ToList();

            int remainingCount = 10 - userQuestions.Count;

            if (remainingCount > 0)
            {
                var otherQuestions = allQuestions
                    .Where(q => q.UserID != currentUserId)
                    .Take(remainingCount)
                    .ToList();

                userQuestions.AddRange(otherQuestions);
            }

            var top3Users = userService.GetUsers()
                .OrderByDescending(u => u.Score)
                .Take(3)
                .ToList();

            ViewBag.TopContributors = top3Users;
            var model = new HomeIndexViewModel
            {
                Questions = userQuestions,
                AdminAnnouncements = announcementByAdmin
            };

            return View(model);
        }

        [Route("About")]
        public ActionResult About()
        {
            return View();
        }



        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> questions = this.questionService.GetQuestions().ToList();
            return View(questions);
        }
        [Route("Categories")]
        public ActionResult Categories()
        {
            List<CategoryViewModel> categories = this.categoryService.GetCategories().ToList();
            return View(categories);
        }
        public ActionResult Search(string search)
        {
            if (search == null || search == "")
            {
                return RedirectToAction("Index", "Home");
            }
            List<QuestionViewModel> searchQ = this.questionService.GetQuestions().Where(q => q.QuestionName.ToLower()
            .Contains(search.ToLower().Trim()) || q.Categories.CatagoryName.ToLower().Contains(search.ToLower().Trim())).ToList();
            ViewBag.search = search;
            return View(searchQ);
        }

      
    }
}