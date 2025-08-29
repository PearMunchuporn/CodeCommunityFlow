using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.CustomFilter;
namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    [AuthorizationAdminOnly]
    public class AdminUsersController : Controller
    {
        // GET: Admin/AdminUser
        IAdminUserService adminUserService;

        public AdminUsersController(IAdminUserService adminUserService)
        {
            this.adminUserService = adminUserService;
        }
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult ChangePassword()
        {
            int admin_id = Convert.ToInt32(Session["CurrentAdminID"]);
            AdminUserViewModels adminUserViewModels = this.adminUserService.GetAdminUserById(admin_id);
            AdminEditPasswordViewModel edit_password = new AdminEditPasswordViewModel()
            {

                Email = adminUserViewModels.Email,
                AdminID = adminUserViewModels.AdminID,
                Password = "",
                PasswordConfirm = ""
            };
        
            return View(edit_password);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(AdminEditPasswordViewModel edit_password)
        {
            if (ModelState.IsValid)
            {
                edit_password.AdminID = Convert.ToInt32(Session["CurrentAdminID"]);
                this.adminUserService.UpdatePasswordAdmin(edit_password);
                TempData["SuccessChangePassword"] = "Your password has been changed successfully..";
                return RedirectToAction("AdminUsers", "AdminUsers");

            }
            else
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(edit_password);
            }
        }

        public ActionResult AdminUsers()
        {
            List<AdminUserViewModels> adminUser = this.adminUserService.GetAdminUsers().ToList();
            return View(adminUser);
        }
      

    }
}