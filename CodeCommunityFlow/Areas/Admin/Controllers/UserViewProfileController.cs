using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    public class UserViewProfileController : Controller
    {
        // GET: Admin/UserViewProfile
        IUserService userService;
        IAnswerService answerService;
        IQuestionService questionService;
        IDeleteLogsService deleteLogsService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        public UserViewProfileController(IUserService ius, IAnswerService ias, IQuestionService iqs, 
            IDeleteLogsService idls, IScoreHistoryLogsService scoreHistoryLogsService)
        {
            this.userService = ius;
            this.answerService = ias;
            this.questionService = iqs;
            this.deleteLogsService = idls;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
        }
        public ActionResult UserViewProfile(int id)
        {
            var User = userService.GetUserByUserId(id);
            var Question = this.questionService.GetQuestions();
            var Answer = this.answerService.GetAnswers();
            UserViewModel TopContributor = this.userService.GetUsers().OrderByDescending(s => s.Score).Take(1).FirstOrDefault();
            bool isTopUser = TopContributor != null && TopContributor.UserID == id;
            ViewBag.TopContributor = TopContributor;
            ViewBag.IsTopUser = isTopUser;
            var DeleteLog = deleteLogsService.GetDeleteLogs();
            var viewModel = new UserProfileViewModel
            {
                User = User,
                UserID = User.UserID,
                UserName = User.UserName,
                Questions = questionService.GetQuestions().Where(q => q.UserID == id).ToList(),
                Answers = answerService.GetAnswers().Where(a => a.UserID == id).ToList(),
                DeleteLogs = deleteLogsService.GetDeleteLogs().Where(d => d.UserID.HasValue && d.UserID.Value == id).ToList(),
                ScoreHistoryUser = scoreHistoryLogsService.GetScoreHistoryLogs().Where(u => u.UserID == id).ToList(),
                ImageUser = User.ImageUser,
                Score =User.Score,
                Email =User.Email,
                WarningCount =User.WarningCount,
                WarningMessage = User.WarningMessage,
                
            };

            return View(viewModel);
        }
    }
}