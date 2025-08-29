using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
using CodeCommunityFlow.CustomFilter;

namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    [AuthorizationAdminOnly]
    [Authorization]
    public class QuestionsManagementController : Controller
    {
        // GET: Admin/QuestionsManagement

        IQuestionService questionService;
        IDeleteLogsService deleteLogsService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        IAdminLogsActionService adminLogsActionService;
        CodeCommunityFlowDbContext db;
        public QuestionsManagementController(IQuestionService iqs , CodeCommunityFlowDbContext db,
            IDeleteLogsService idls, IScoreHistoryLogsService scoreHistoryLogsService , IAdminLogsActionService adminLogsActionService)
        {
            this.questionService = iqs;
            this.db = db;
            this.deleteLogsService = idls;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
            this.adminLogsActionService = adminLogsActionService;
        }

        public ActionResult QuestionManagement()
        {
            List<QuestionViewModel> questions = this.questionService.GetQuestions().ToList();
            return View(questions);
        }

        public ActionResult DeleteQuestionByAdmin(int id)
        {

            var DeleteQuestion = db.Questions.Where(a => a.QuestionID == id).FirstOrDefault();
            int UserID = DeleteQuestion.UserID;
           
            this.questionService.DeleteQuestionByAdmin(id , UserID);

            var DeleteLogQuestion = new DeleteLogsViewModel
            {
                QuestionID = id,
                AnswerID = null,
                DeleteByAdmin = true,
                UserID = UserID,
                DeleteDateTime = DateTime.Now,
                DeletionType = "Question"

            };
            this.deleteLogsService.InsertDeleteLogs(DeleteLogQuestion);
            var DeleteByAdminScoreLog = new ScoreHistoryViewModel
            {
                UserID = UserID,
                DeletedByAdmin = -50,
            };
            this.scoreHistoryLogsService.InsertScoreHistory(DeleteByAdminScoreLog);
            var user = db.Users.Where(a => a.UserID == UserID).FirstOrDefault();
            var UserName = user.UserName;
            var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
            var AddActionLogs = new AdminActionLogsViewModel
            {
                ActionByAdminID = adminId,
                ActionName = "Delete User's Question",
                ActionDateTime = DateTime.Now,
                UserID = UserID,
                UserName = UserName,

            };

            this.adminLogsActionService.InsertAction(AddActionLogs);
            TempData["SuccessDeleteQuestion"] = "This question has been deleted successfully.";
            return RedirectToAction("QuestionManagement", "QuestionsManagement", new { area = "Admin" });
        }
    }
}