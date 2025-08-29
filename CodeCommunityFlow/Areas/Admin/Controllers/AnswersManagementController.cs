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
    public class AnswersManagementController : Controller
    {
        // GET: Admin/AnswersManagement
        IAnswerService answerService;
        IDeleteLogsService deleteLogsService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        IAdminLogsActionService adminLogsActionService;
        CodeCommunityFlowDbContext db;
        public AnswersManagementController(IAnswerService answerService, CodeCommunityFlowDbContext db,
            IDeleteLogsService deleteLogsService, IScoreHistoryLogsService scoreHistoryLogsService, IAdminLogsActionService adminLogsActionService)
        {
            this.answerService = answerService;
            this.db = db;
            this.deleteLogsService = deleteLogsService;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
            this.adminLogsActionService = adminLogsActionService;
        }
        public ActionResult AnswerManagement()
        {
            List<AnswerViewModel> answers = this.answerService.GetAnswers().ToList();

            return View(answers);
        }
      
        public ActionResult DeleteAnswerByAdmin(int id)
        {
            var DeleteAnswer = db.Answers.Where(a => a.AnswerID == id).FirstOrDefault();
            
            int UserID = DeleteAnswer.UserID.Value;

            var user = db.Users.Where(a => a.UserID == UserID).FirstOrDefault();
            var userName = user.UserName;
            
            this.answerService.DeleteAnswerByAdmin(id, UserID);

            var deleteLogAnswer = new DeleteLogsViewModel
            {
                AnswerID = id,
                QuestionID = DeleteAnswer.QuestionID,
                UserID = UserID,
                DeleteDateTime = DateTime.Now,
                DeleteByAdmin = true,
                DeletionType ="Answer"
            };
            this.deleteLogsService.InsertDeleteLogs(deleteLogAnswer);

            var DeletedByAdminScoreLog = new ScoreHistoryViewModel
            {
                UserID = UserID,
                DeletedByAdmin = -50,
            };

            var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
            var AddActionLogs = new AdminActionLogsViewModel
            {
                ActionByAdminID = adminId,
                ActionName = "Delete User's Answer",
                ActionDateTime = DateTime.Now,
                UserID = UserID,
                UserName = userName

            };

            this.adminLogsActionService.InsertAction(AddActionLogs);
            this.scoreHistoryLogsService.InsertScoreHistory(DeletedByAdminScoreLog);

            TempData["SuccessDeleteAnswer"] = "This answer has been deleted successfully.";

            return RedirectToAction("AnswerManagement", "AnswersManagement", new { Area = "Admin" });
        }
    }
}