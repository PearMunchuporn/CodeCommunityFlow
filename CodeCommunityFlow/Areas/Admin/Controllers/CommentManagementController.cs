using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.DomainModels;
namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    public class CommentManagementController : Controller
    {
        // GET: Admin/CommentManagement
        ICommentFromAnnouncementService commentService;
        CodeCommunityFlowDbContext db;
        IDeleteLogsService deleteLogsService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        IAdminLogsActionService adminLogsActionService;
        public CommentManagementController(ICommentFromAnnouncementService commentService, 
            CodeCommunityFlowDbContext db, IDeleteLogsService deleteLogsService , IScoreHistoryLogsService scoreHistoryLogsService,
            IAdminLogsActionService adminLogsActionService)
        {
            this.commentService = commentService;
            this.db = db;
            this.deleteLogsService = deleteLogsService;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
            this.adminLogsActionService = adminLogsActionService;
        }
        public ActionResult CommentsManagement()
        {
            List<CommentFromAnnouncementViewModel> comments = this.commentService.GetComment().ToList();
            return View(comments);
        }

        public ActionResult DeleteCommentByAdmin(int id)
        {
            var DeleteComment = db.CommentFromAnnouncement.Where(a => a.CommentID == id).FirstOrDefault();

            int UserID = DeleteComment.UserID.Value;
            var user = db.Users.Where(a => a.UserID == UserID).FirstOrDefault();
            var UserName = user.UserName;

            this.commentService.DeleteCommentByAdmin(id, UserID);

            var deleteLogAnswer = new DeleteLogsViewModel
            {
          
                CommentID = id,
                UserID = UserID,
                DeleteDateTime = DateTime.Now,
                DeleteByAdmin = true,
                DeletionType = "Comment"
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
                ActionName = "Delete User's Comment",
                ActionDateTime = DateTime.Now,
                UserID = UserID,
                UserName = UserName,

            };

            this.adminLogsActionService.InsertAction(AddActionLogs);
            this.scoreHistoryLogsService.InsertScoreHistory(DeletedByAdminScoreLog);

            TempData["SuccessDeleteComment"] = "This comment has been deleted successfully.";

            return RedirectToAction("CommentsManagement", "CommentManagement", new { Area = "Admin" });
        }
    }
}