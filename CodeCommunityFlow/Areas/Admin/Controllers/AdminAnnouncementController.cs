using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.ViewModelFiles;
using System.IO;
using CodeCommunityFlow.CustomFilter;
using CodeCommunityFlow.DomainModels;
namespace CodeCommunityFlow.Areas.Admin.Controllers
{ 
    [AuthorizationAdminOnly]
    public class AdminAnnouncementController : Controller
    {
        // GET: Admin/AdminAnnounce
        IAdminAnnounceService adminAnnounceService;
        IAdminLogsActionService adminLogsActionService;
        CodeCommunityFlowDbContext db;
        public AdminAnnouncementController(IAdminAnnounceService adminAnnounceService, CodeCommunityFlowDbContext db, 
            IAdminLogsActionService adminLogsActionService)
        {
            this.adminAnnounceService = adminAnnounceService;
            this.db = db;
            this.adminLogsActionService = adminLogsActionService;
        }

        /// Method
        /// 1. Annoucement [GET] View 
        /// 2. AddAnnouncement [GET]
        /// 3. ViewAnnouncement (Each Annoucement)
        /// 4. AddAnnouncement [GET]
        /// 5. AddAnnouncement [POST]
        /// 6. EditAnnouncement [GET]
        /// 7. EditAnnouncement [POST]
        /// 8. DeleteAnouncement 
        public ActionResult Annoucement()
        {
            List<AdminAnnouncementViewModels> adminAnnouncements = this.adminAnnounceService.GetAnnoucement().ToList();
            return View(adminAnnouncements);
        }
        public ActionResult ViewAnnouncement(int id)
        {
            var AdminId = Convert.ToInt32(Session["CurrentAdminID"]);
            this.adminAnnounceService.UpdateAnnouncementViewCount(id, 1);
            var announce = this.adminAnnounceService.GetAnnouncementByID(id, AdminId);
            return View(announce);
        }

        [HttpGet]
        public ActionResult AddAnnouncement()
        {

            return View();
        }
        
       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnnouncement(AddImgForAnnouncementByAdmin addNewAnnouncement)
        {
            if (ModelState.IsValid)
            {
                addNewAnnouncement.AdminID = Convert.ToInt32(Session["CurrentAdminID"]);

                addNewAnnouncement.AnnouncementDateTime = DateTime.Now;
                addNewAnnouncement.CommentCount = 0;
                addNewAnnouncement.ViewCount = 0;
                addNewAnnouncement.VoteCount = 0;
                if (ModelState.IsValid)
                {

                    List<string> img = new List<string>();

                    if (addNewAnnouncement.Files != null && addNewAnnouncement.Files.Any())
                    {
                        foreach (var file in addNewAnnouncement.Files)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                                var path = Path.Combine(Server.MapPath("~/Uploads/AddImgForAnnouncementByAdmin"), uniqueName);

                                Directory.CreateDirectory(Path.GetDirectoryName(path));
                                file.SaveAs(path);

                                img.Add("/Uploads/AddImgForAnnouncementByAdmin/" + uniqueName);
                            }
                        }
                    }

                    var AddNewAnnouncment = new AnnouncementCreateViewModel
                    {
                        AdminID = addNewAnnouncement.AdminID,
                        AdminAnnounceContent = addNewAnnouncement.AdminAnnounceContent,
                        AdminAnnounceTopic = addNewAnnouncement.AdminAnnounceTopic,
                        ImageContent = string.Join(",", img),
                        VoteCount = addNewAnnouncement.VoteCount,
                        ViewCount = addNewAnnouncement.ViewCount,
                        CommentCount = addNewAnnouncement.CommentCount,
                        AnnouncementDateTime = addNewAnnouncement.AnnouncementDateTime,
                        Category = "Admin Announcement"

                    };
                    var AddActionLogs = new AdminActionLogsViewModel
                    {
                        ActionByAdminID = addNewAnnouncement.AdminID,
                        ActionName ="Post Announcement",
                        ActionDateTime = DateTime.Now,
                        
                    };

                    this.adminAnnounceService.InsertAnnoumentByAdmin(AddNewAnnouncment);
                    this.adminLogsActionService.InsertAction(AddActionLogs);
                    return RedirectToAction("Annoucement", "AdminAnnouncement", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("My Error", "Cannot add an announcement, please try again.");
                }
              
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditAnnouncement(int id)
        {
            var Admin_id = Convert.ToInt32(Session["CurrentAdminID"]);
            AdminAnnouncementViewModels Annnouncement = this.adminAnnounceService.GetAnnouncementByID(id, Admin_id);
            if (Annnouncement.AdminID != Admin_id)
            {

                return RedirectToAction("ViewAnnouncement", new { id = id,  area = "Admin" });
            }

            return View(Annnouncement);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAnnouncement(UpdateImgAnnouncementByAdmin update_Announcement, string ImgOld, string[] CheckDeleteImage)
        {
            var Admin_id = Convert.ToInt32(Session["CurrentAdminID"]);
            var Annoumcement = this.adminAnnounceService.GetAnnouncementByID(update_Announcement.AdminAnnouncementID ,Admin_id);
            update_Announcement.AnnouncementDateTime = DateTime.Now;
            update_Announcement.ViewCount = Annoumcement.ViewCount;
            update_Announcement.VoteCount = Annoumcement.VoteCount;
            var id = Annoumcement.AdminAnnouncementID;

            if (ModelState.IsValid)
            {
                List<string> oldPaths = new List<string>();

                if (!string.IsNullOrEmpty(update_Announcement.ImgOld))
                {
                    oldPaths = update_Announcement.ImgOld.Split(';').ToList();
                }

                if (CheckDeleteImage != null)
                {
                    foreach (var path in CheckDeleteImage)
                    {
                        if (oldPaths.Contains(path))
                        {
                            oldPaths.Remove(path); // remove from the list

                            var fullPath = Server.MapPath(path);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }
                    }
                }


                List<string> newImages = new List<string>();
                if (update_Announcement.Files != null && update_Announcement.Files.Any(f => f != null && f.ContentLength > 0))
                {
                    foreach (var file in update_Announcement.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/AddImgForAnnouncementByAdmin"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);

                            newImages.Add("/Uploads/AddImgForAnnouncementByAdmin/" + uniqueName);
                        }
                    }
                }
                var allImages = oldPaths.Concat(newImages).ToList();
                var UpdateAnnouncement = new AnnouncementUpdateViewModel
                {
                    ImageContent = string.Join(";", allImages),
                    AdminAnnounceContent = update_Announcement.AdminAnnounceContent,
                    AnnouncementDateTime = update_Announcement.AnnouncementDateTime,
                    AdminAnnounceTopic = update_Announcement.AdminAnnounceTopic,
                    AdminID = Admin_id,
                    VoteCount = update_Announcement.VoteCount,
                    CommentCount = update_Announcement.CommentCount,
                    ViewCount = update_Announcement.ViewCount,
                 
                    AdminAnnouncementID = update_Announcement.AdminAnnouncementID,
                    Description = "Edited Announcement",
                    Category = "Admin Announcement"


                };

                var AddActionLogs = new AdminActionLogsViewModel
                {
                    ActionByAdminID = Admin_id,
                    ActionName = "Update Announcement",
                    ActionDateTime = DateTime.Now,

                };
                this.adminAnnounceService.UpdateAnnoucement(UpdateAnnouncement);
                this.adminLogsActionService.InsertAction(AddActionLogs);
                return RedirectToAction("ViewAnnouncement", "AdminAnnouncement", new { id,   area = "Admin"  });
            }

            else
            {
                ModelState.AddModelError("My Error", "Cannot edit the question, please try again.");
                return RedirectToAction("ViewAnnouncement", new { id, area = "Admin" });
            }

        }

        public ActionResult DeleteAnnouncement(int id)
        {
           
            this.adminAnnounceService.DeleteAnnouncement(id);
            var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
            var AddActionLogs = new AdminActionLogsViewModel
            {
                ActionByAdminID = adminId,
                ActionName = "Delete Announcement",
                ActionDateTime = DateTime.Now,

            };
   
            this.adminLogsActionService.InsertAction(AddActionLogs);
            TempData["DelectAnnouce"] = "Your Annoucement has been deleted successfully";
            return RedirectToAction("Annoucement", "AdminAnnouncement" , new { area = "Admin" });

        }


    }

}