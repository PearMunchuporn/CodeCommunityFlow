using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.CustomFilter;
using CodeCommunityFlow.ViewModelFiles;
using System.IO;

namespace CodeCommunityFlow.Controllers
{      
    public class AccountController : Controller
    {
        // GET: Default
        IUserService userService;
        IAdminUserService adminUserService;
        public AccountController(IUserService us, IAdminUserService admin)
        {
            this.userService = us;
            this.adminUserService = admin;
        }

        //Method order
        //1. Register [GET]
        //2. Register [POST]
        //3. Login [GET]
        //4. Login [POST]
        //5. Logout 
        //6. ChangeProfile [GET]
        //7. ChangeProfile [POST]
        //8. ChangePassword [GET]
        //9. ChangePassword [POST]
        //10. MyProfile (View My Profile)
        //11. UserDetails (View other users' profile)
        //12. DeleteMyAccount
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]

        public ActionResult Register(RegisterViewModel register)
        {

            if (ModelState.IsValid)
            {

                int userId = this.userService.InsertUser(register);
                Session["CurrentUserName"] = register.UserName;
                Session["CurrentUserID"] = userId;
                Session["CurrentUserEmail"] = register.Email;
                Session["CurrentUserPassword"] = register.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot Register, Please try to register again!");

                return View();
            }


        }
        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            var model = new LoginViewModel();

            if (Session["CurrentUserIsAdmin"] != null)
            {
                if ((bool)Session["CurrentUserIsAdmin"] == true && Session["CurrentAdminID"] != null)
                {
                    return RedirectToRoute(new { area = "Admin", controller = "Home", action = "Index" });
                }
                else if ((bool)Session["CurrentUserIsAdmin"] == false && Session["CurrentUserID"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public ActionResult Login(LoginViewModel login)

        {
            if (ModelState.IsValid)
            {
                var useriewModel = this.userService.GetUserByEmailAndPassword(login.Email, login.Password);
                var adminUser = this.adminUserService.GetAdminUserByEmailandPassword(login.Email, login.Password);

                if (adminUser != null)
                {
                    Session["CurrentAdminID"] = adminUser.AdminID;
                    Session["CurrentAdminName"] = adminUser.AdminName;
                    Session["CurrentAminPassword"] = adminUser.Password;
                    Session["CurrentAdminEmail"] = adminUser.Email;    
                    Session["CurrentUserIsAdmin"] = true;
                    return RedirectToRoute(new { area = "Admin", controller = "Home", action = "AdminIndex" });
                  
                   
                }
                else if (useriewModel != null)
                {
                    Session["CurrentUserName"] = useriewModel.UserName;
                    Session["CurrentUserID"] = useriewModel.UserID;
                    Session["CurrentUserEmail"] = useriewModel.Email;
                    Session["CurrentUserPassword"] = useriewModel.Password;
                    Session["CurrentUserImage"] = useriewModel.ImageUser;
                    Session["CurrentUserIsAdmin"] = false;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("My Error",
                        "Your Email or Password is incorrect, Please try again.");
                    return View(login);
                }
            }
            else
            {
                ModelState.AddModelError("My Error", " Please try to login again");
            }

            return View(login);
        }
        [Route("Logout")]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [AuthorizationAttribute]
        [Route("MyAccount/ChangeMyProfile")]

        public ActionResult ChangeProfile()
        {
            int user_id = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel useriewModel = this.userService.GetUserByUserId(user_id);

     
            UpdateUserImage edit_user = new UpdateUserImage()
            {
                UserName = useriewModel.UserName,
                Email = useriewModel.Email,
                UserID = useriewModel.UserID,
                Score = useriewModel.Score,
                WarningCount = useriewModel.WarningCount,
                Password = useriewModel.Password,
                ImageUser = useriewModel.ImageUser,
                isAdmin = useriewModel.isAdmin,
                WarningMessage = useriewModel.WarningMessage,
                FilesImageUser = new List<HttpPostedFileBase>() 
            };

            return View(edit_user);
        }
        [Route("MyAccount/ChangeMyProfile")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationAttribute]
        public ActionResult ChangeProfile(UpdateUserImage edit_user ,string ImageUserOld)
        {
            int user_id = Convert.ToInt32(Session["CurrentUserID"]);
            var user = userService.GetUserByUserId(user_id);

      

            edit_user.Score = user.Score;
            edit_user.WarningCount = user.WarningCount;
   
            edit_user.isAdmin = user.IsAdmin;
       

            if (ModelState.IsValid)
            {
                string OdlImg = null;

                if (!string.IsNullOrEmpty(edit_user.ImageUserOld))
                {
                    OdlImg = edit_user.ImageUserOld;
                    edit_user.ImageUser = OdlImg;
                }

       
                if (edit_user.FilesImageUser != null && edit_user.FilesImageUser.Any(f => f != null && f.ContentLength > 0))
                {
                    foreach (var file in edit_user.FilesImageUser)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks  + "_" + edit_user.UserName +"_Img"+ fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/ImageUsers"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);
                      
                            edit_user.ImageUser = uniqueName;

                        }
                    }
                }
                Session["CurrentUserName"] = edit_user.UserName;
                Session["CurrentUserImage"] = edit_user.ImageUser;
                Session["CurrentUserImageVersion"] = DateTime.Now.Ticks.ToString();

                var UpdateProfileUser = new UpdateUserImage
                {
                    UserID = user_id,
                    Password = edit_user.Password,
                    isAdmin = edit_user.isAdmin,
                    Score = edit_user.Score,
                    UserName = edit_user.UserName,
                    WarningCount = edit_user.WarningCount,
                    WarningMessage = edit_user.WarningMessage,
                    ImageUser = edit_user.ImageUser,
                    Email = edit_user.Email
                  
                };
                this.userService.UpdateUserDetails(UpdateProfileUser);
                TempData["SuccessChangeProfile"] = "Your profile has been changed successfully.";
                return RedirectToAction("MyProfile", "Account");
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(edit_user);
            }
          
        }
        [AuthorizationAttribute]
        [Route("MyAccount/ChangeMyPassword")]
        public ActionResult ChangePassword()
        {
            int user_id = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel userViewModel = this.userService.GetUserByUserId(user_id);
            EditUserPasswordViewModel edit_password = new EditUserPasswordViewModel()
            {

                Email = userViewModel.Email,
                UserID = userViewModel.UserID,
                Password = "",
                PasswordConfirm = ""
            };
            TempData["SuccessChangePassword"] = "Your password has been changed successfully.";
            return View(edit_password);
        }

        [HttpPost]
        [Route("MyAccount/ChangeMyPassword")]
        [ValidateAntiForgeryToken]
        [AuthorizationAttribute]

        public ActionResult ChangePassword(EditUserPasswordViewModel edit_password)
        {
            if (ModelState.IsValid)
            {
                edit_password.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.userService.UpdateUserPassword(edit_password);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(edit_password);
            }
        }

        [AuthorizationAttribute]
   
        public ActionResult MyProfile()
            
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int user_id = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel userViewModel = this.userService.GetUserByUserId(user_id);
            UserViewModel TopContributor = this.userService.GetUsers().OrderByDescending(s => s.Score).Take(1).FirstOrDefault();
            bool isTopUser = TopContributor != null && TopContributor.UserID == user_id;
            ViewBag.TopContributor = TopContributor;
            ViewBag.IsTopUser = isTopUser;
            return View(userViewModel);

        }
        [Route("UserProfile/{id}")]
       
        public ActionResult UserDetails(int id)
        {

            UserViewModel userViewModel = this.userService.GetUserByUserId(id);
            UserViewModel TopContributor = this.userService.GetUsers().OrderByDescending(s => s.Score).Take(1).FirstOrDefault();
            bool isTopUser = TopContributor != null && TopContributor.UserID == id;
            ViewBag.TopContributor = TopContributor;
            ViewBag.IsTopUser = isTopUser;
            return View(userViewModel);
        }

        public ActionResult DeleteMyAccount(int id)
        {
            this.userService.DeleteUser(id);
            Session.Clear();        
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}