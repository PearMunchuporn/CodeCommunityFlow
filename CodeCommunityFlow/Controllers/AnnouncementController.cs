using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.SelectList;
using CodeCommunityFlow.ViewModelFiles;
using CodeCommunityFlow.ServiceLayers;
using System.IO;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.CustomFilter;

namespace CodeCommunityFlow.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        IAdminAnnounceService adminAnnounceService;
        IReportService reportService;
        ICommentFromAnnouncementService commentService;
        CodeCommunityFlowDbContext db;
        IDeleteLogsService deleteLogsService;
        IReportLogsService reportLogsService;
        IUserService userService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        public AnnouncementController(IAdminAnnounceService adminAnnounceService, IReportService reportService,
            ICommentFromAnnouncementService commentService, CodeCommunityFlowDbContext db,
            IDeleteLogsService deleteLogsService, IReportLogsService reportLogsService , IUserService userService, IScoreHistoryLogsService scoreHistoryLogsService)
        {
            this.adminAnnounceService = adminAnnounceService;
            this.reportService = reportService;
            this.commentService = commentService;
            this.db = db;
            this.deleteLogsService = deleteLogsService;
            this.reportLogsService = reportLogsService;
            this.userService = userService;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
        }
        public ActionResult AnnouncementByAdminView(int id)
        {
            adminAnnounceService.UpdateAnnouncementViewCount(id, 1); //count views
            var ThisAnnouncement = adminAnnounceService.GetAnnoucement().Where(a => a.AdminAnnouncementID == id).FirstOrDefault();
            var AdminId = ThisAnnouncement.AdminID;
            AdminAnnouncementViewModels annoucement = adminAnnounceService.GetAnnouncementByID(id, AdminId);

            var reportReasons = this.reportService.GetReports();

            var reportReasonList = reportReasons.Select(r => new SelectListItem
            {
                Value = r.ReportID.ToString(),
                Text = r.ReportReason
            }).ToList();

            var ViewAnnounce = new CodeCommunityFlow.SelectList.SelectReportComment
            {
                AdminAnnouncementID = annoucement.AdminAnnouncementID,
                AdminAnnounceTopic = annoucement.AdminAnnounceTopic,
                AdminAnnounceContent = annoucement.AdminAnnounceContent,
                AnnouncementDateTime = annoucement.AnnouncementDateTime,
                UserID = annoucement.UserID,
                AdminID =annoucement.AdminID,
                VoteCount = annoucement.VoteCount,
                CommentCount = annoucement.CommentCount,
                ViewCount = annoucement.ViewCount,
                ImageContent = annoucement.ImageContent,
                ReportReasonSelectList = reportReasonList,
                CommentFromAnnouncement = annoucement.CommentFromAnnouncement,
                AdminUsers = annoucement.AdminUsers,
                Users = annoucement.Users,
                CurrentUserVoteType = annoucement.CurrentUserVoteType,
                ReportReason = annoucement.ReportReason,
                Description = annoucement.Description,
                Category = annoucement.Category
            };

            return View(ViewAnnounce);

        }
        [AuthorizationAttribute]
        public ActionResult AddComment(AddCommentImg Addcomment)
        {
            Addcomment.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            Addcomment.CommentDateTime = DateTime.Now;
            Addcomment.VoteCount = 0;
            var ThisAnnouncement = this.adminAnnounceService.GetAnnoucement().Where(a => a.AdminAnnouncementID == Addcomment.AdminAnnouncementID).FirstOrDefault();
            var adminId = ThisAnnouncement.AdminUsers.AdminID;

            if (ModelState.IsValid)
            {
                List<string> img = new List<string>();

                if (Addcomment.Files != null && Addcomment.Files.Any())
                {
                    foreach (var file in Addcomment.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/ImageComment"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);

                            img.Add("/Uploads/ImageComment/" + uniqueName);
                        }
                    }
                }

                var InsertNewComment = new AddCommentViewModel
                {
                    CommentContent = Addcomment.CommentContent,
                    CommentDateTime = Addcomment.CommentDateTime,
                    AdminAnnouncementID = Addcomment.AdminAnnouncementID,
                    UserID = Addcomment.UserID,
                    VoteCount = Addcomment.VoteCount,
                    Image = string.Join(";", img),
                    

                };


                commentService.InsertComment(InsertNewComment);
                adminAnnounceService.UpdateCommentCount(Addcomment.AdminAnnouncementID, 1);
              
                return RedirectToAction("AnnouncementByAdminView", "Announcement", new { id = InsertNewComment.AdminAnnouncementID });
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot answer the question, please try again.");

                var AnnouncementViewModel = adminAnnounceService.GetAnnouncementByID(Addcomment.AdminAnnouncementID, adminId);
                return View("AnnouncementByAdminView", "Announcement", AnnouncementViewModel);
            }
        }
        [AuthorizationAttribute]
        public ActionResult EditComment(EditImgComment edit_comment, string ImageOld, string[] CheckDeleteImage = null)
        {
            var comment = this.commentService.GetCommentByCommentId(edit_comment.CommentID);
            edit_comment.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            edit_comment.CommentDateTime = DateTime.Now;
            edit_comment.VoteCount = comment.VoteCount;

            if (ModelState.IsValid)
            {

                List<string> oldPaths = new List<string>();

                if (!string.IsNullOrEmpty(edit_comment.ImageOld))
                {
                    oldPaths = edit_comment.ImageOld.Split(';').ToList();
                }

                // delete just only checked img
                if (CheckDeleteImage != null)
                {
                    foreach (var del in CheckDeleteImage)
                    {
                        if (oldPaths.Contains(del))
                        {
                            oldPaths.Remove(del);

                            var serverPath = Server.MapPath(del);
                            if (System.IO.File.Exists(serverPath))
                            {
                                System.IO.File.Delete(serverPath);
                            }
                        }
                    }
                }


                List<string> newImgs = new List<string>();

                if (edit_comment.Files != null && edit_comment.Files.Any(f => f != null && f.ContentLength > 0))
                {
                    foreach (var file in edit_comment.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/ImageComment"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);

                            newImgs.Add("/Uploads/ImageComment/" + uniqueName);
                        }
                    }
                }

                var allImages = oldPaths.Concat(newImgs).ToList();
                var UpdateComment = new UpdateCommentViewModel
                {
                    CommentID = edit_comment.CommentID,
                    CommentContent = edit_comment.CommentContent,
                    CommentDateTime = edit_comment.CommentDateTime,
                    AdminAnnouncementID = edit_comment.AdminAnnouncementID,
                    UserID = edit_comment.UserID,
                    VoteCount = edit_comment.VoteCount,
                    Image = string.Join(";", allImages),
                    Description = "Edited Comment"
                };

                commentService.UpdateComment(UpdateComment);
               
                return RedirectToAction("AnnouncementByAdminView", "Announcement", new { id = edit_comment.AdminAnnouncementID });
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot answer the question, please try again.");



                return RedirectToAction("AnnouncementByAdminView", "Announcement", new { id = edit_comment.AdminAnnouncementID });
            }
        }
        [AuthorizationAttribute]
        public ActionResult DeleteComment(int id)
        {

            var Comment = db.CommentFromAnnouncement.Where(a => a.CommentID == id).FirstOrDefault();
            int AnnounceID = Comment.AdminAnnouncementID;

            int userId = Comment.UserID.Value;

            var deleteLogComment = new DeleteLogsViewModel
            {
                CommentID = id,
                UserID = userId,
                DeleteDateTime = DateTime.Now,
                DeleteByAdmin = false,
                DeletionType = "Comment"
            };

            this.commentService.DeleteComment(id);
            this.deleteLogsService.InsertDeleteLogs(deleteLogComment);
            TempData["SuccessDeleteComment"] = "Delete your comment successfully.";

            this.commentService.DeleteComment(id);
            return RedirectToAction("AnnouncementByAdminView", "Announcement", new { id = Comment.AdminAnnouncementID });
        }

        [AuthorizationAttribute]
        public ActionResult ReportComment(int commentId, int reasonId, int announceId ,int reportedUserId)
        {
            int ReportedByUserID = Convert.ToInt32(Session["CurrentUserID"]);
            var report = new ReportLogsViewModel
            {
                CommentID = commentId,
            
                ReportReasonID = reasonId,
                ReportedByUserID = ReportedByUserID, // who is reporting
                ReportedUserID = reportedUserId, // who being reported
                ReportedTime = DateTime.Now,
                ReportType = "Reported Comment"
            };

            var UserGotReportScore = new ScoreHistoryViewModel
            {
                UserID = reportedUserId,
                ReportReasonID = reasonId,
            };
            this.reportLogsService.InsertReportLogs(report);
            this.userService.UserUpdateScoreWhenGotReport(reportedUserId, reasonId);
            this.scoreHistoryLogsService.InsertScoreHistory(UserGotReportScore);

            TempData["SuccessReport"] = "Report submitted successfully.";
            return RedirectToAction("AnnouncementByAdminView", "Announcement", new { id = announceId });
        }
    }
}