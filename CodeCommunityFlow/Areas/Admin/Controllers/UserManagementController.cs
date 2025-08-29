using CodeCommunityFlow.CustomFilter;
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
    public class UserManagementController : Controller
    {
        // GET: Admin/UserManagement
        IUserService userService;
        IReportService reportService;
        IReportLogsService reportLogsService;
        IAnswerService answerService;
        IQuestionService questionService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        IAdminLogsActionService adminLogsActionService;
        public UserManagementController(IUserService userService, IReportService reportService, 
            IReportLogsService reportLogsService, IAnswerService answerService, 
            IQuestionService questionService, IScoreHistoryLogsService scoreHistoryLogsService , IAdminLogsActionService adminLogsActionService) {
            this.userService = userService;
            this.reportService = reportService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.reportLogsService = reportLogsService;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
            this.adminLogsActionService =adminLogsActionService;
       }

        //Method order
        //1. UsersManagement (View)
        //2. DeleteUserByAdmin
        //3. SentMessageToUser (from user management page)
        //4. WarnsUser [GET]
        //5. WarnsUser [POST]
        //6. UpdateUserScore [GET]
        //7. UpdateUserScore [POST]
        public ActionResult UsersManagement()
        {
            List<UserViewModel> users = this.userService.GetUsers().ToList();
            return View(users);
        }
 
        public ActionResult DeleteUserByAdmin(int id)
        {
            var User = userService.GetUserByUserId(id);
            var UserName = User.UserName;
            this.userService.DeleteUser(id);
        
            var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
            var AddActionLogs = new AdminActionLogsViewModel
            {
                ActionByAdminID = adminId,
                ActionName = "Delete User",
                ActionDateTime = DateTime.Now,
                UserID = id,
                UserName = UserName

            };

            this.adminLogsActionService.InsertAction(AddActionLogs);

            TempData["SuccessDelete"] = "Delete user successfully.";
            return RedirectToAction("UsersManagement", "UserManagement", new { Area = "Admin" });
        }
        [HttpPost]
        public JsonResult SentMessageToUser(int UserId, string WarningMessage)
        {
            var user = this.userService.GetUserByUserId(UserId);
            user.WarningMessage = WarningMessage;

            var UserWarnVM = new WarnUserViewModel
            {
                UserID = user.UserID,
                WarningMessage = user.WarningMessage
            };
            userService.InsertMsgToUser(UserWarnVM);
            userService.UpdateScoreWhenUserGotWarning(user.UserID);

            var GotWarnedByAdmin = new ScoreHistoryViewModel
            {
                UserID = user.UserID,
                WarnedByAdmin = -30,
            };

            this.scoreHistoryLogsService.InsertScoreHistory(GotWarnedByAdmin);

            var User = userService.GetUserByUserId(UserId);
            var UserName = User.UserName;
            var adminId = Convert.ToInt32(Session["CurrentAdminID"]);

            var AddActionLogs = new AdminActionLogsViewModel
            {
                ActionByAdminID = adminId,
                ActionName = "Warn User",
                ActionDateTime = DateTime.Now,
                UserID = UserId,
                UserName = UserName

            };

            this.adminLogsActionService.InsertAction(AddActionLogs);
            TempData["SuccessWarn"] = "Send message to warn user successfully.";
            return Json(new
            {
                success = true,
                userId = UserId,
                warningCount = user.WarningCount,
                warningMessage = user.WarningMessage
            });
           
        }

        public ActionResult WarnsUser(int id)
        {
            UserViewModel user = this.userService.GetUserByUserId(id);

            var WarnUVM = new WarnUserViewModel
            {
                UserID = user.UserID,
                UserName = user.UserName,
                ScoreMinus = user.Report?.ScoreMinus ?? 0,
                Score = user.Score,
                WarningCount = user.WarningCount,
                DeletebyAdminCount = user.DeletebyAdminCount,
                WarningMessage = user.WarningMessage,
                ReportReasonTable = reportService.GetReports(),
                Answers = answerService.GetAnswers().Where(a => a.UserID == id).ToList(),
                Questions = questionService.GetQuestions().Where(q=>q.UserID==id).ToList(),
                ReportLogUser = reportLogsService.GetReportLogs().Where(r => r.ReportedUser?.UserID == id).ToList(),
            };

            return View(WarnUVM);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WarnUser(WarnUserViewModel user)
        {
        
            if (ModelState.IsValid)
            {
               
                userService.InsertMsgToUser(user);
                userService.UpdateScoreWhenUserGotWarning(user.UserID);
                var GotWarnedByAdmin = new ScoreHistoryViewModel
                {
                    UserID = user.UserID,
                    WarnedByAdmin = -30,
                };

                var User = userService.GetUserByUserId(user.UserID);
                var UserName = User.UserName;
                var adminId = Convert.ToInt32(Session["CurrentAdminID"]);

                var AddActionLogs = new AdminActionLogsViewModel
                {
                    ActionByAdminID = adminId,
                    ActionName = "Warn User",
                    ActionDateTime = DateTime.Now,
                    UserID = user.UserID,
                    UserName = UserName

                };

                this.adminLogsActionService.InsertAction(AddActionLogs);
                this.scoreHistoryLogsService.InsertScoreHistory(GotWarnedByAdmin);
                TempData["SuccessWarn"] = "Send message to warn user successfully.";
                return RedirectToAction("UsersManagement", "UserManagement", new { Area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot send the message to this user, please try again.");
                return View();
            }
        }

        public ActionResult UpdateUserScore(int id)
        {
            UserViewModel user = this.userService.GetUserByUserId(id);


            var updateScore = new UpdateUserScoreViewModel
            {
                UserID = user.UserID,
                UserName = user.UserName,
                ScoreMinus = user.Report?.ScoreMinus ?? 0,
                Score = user.Score,
                WarningCount = user.WarningCount,
                WarningMessage = user.WarningMessage,
                ReportReasonTable = reportService.GetReports(),
                ReportLogUser = reportLogsService.GetReportLogs().Where(r => r.ReportedUser?.UserID == id).ToList(),
            };
            return View(updateScore);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserScore(UpdateUserScoreViewModel updateScore)
        {
            if(ModelState.IsValid)
            {
                userService.UpdateScoreWhenUserGotUpdateByAmin(updateScore);
                var UpdateScoreByAdmin = new ScoreHistoryViewModel
                {
                    UserID = updateScore.UserID,
                    UpdateScoreByAdmin = updateScore.Score,
                };
                this.scoreHistoryLogsService.InsertScoreHistory(UpdateScoreByAdmin);

                var User = userService.GetUserByUserId(updateScore.UserID);
                var UserName = User.UserName;
                var adminId = Convert.ToInt32(Session["CurrentAdminID"]);

                var AddActionLogs = new AdminActionLogsViewModel
                {
                    ActionByAdminID = adminId,
                    ActionName = "Update Score",
                    ActionDateTime = DateTime.Now,
                    UserID = updateScore.UserID,
                    UserName = UserName

                };

                this.adminLogsActionService.InsertAction(AddActionLogs);

                TempData["SuccessUpdateUserScore"] = "Reimburse user's score successfully.";
                return RedirectToAction("UsersManagement", "UserManagement", new { Area = "Admin" });
             
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot reimburse score to this user, please try again.");
                return View();
            }
        }

    }
}