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
    public class CategoriesManagementController : Controller
    {
        // GET: Admin/Categories
        ICategoryService categoryService;
        IAdminLogsActionService adminLogsActionService;
    

        public CategoriesManagementController(ICategoryService ics, IAdminLogsActionService adminLogsActionService)
        {
            this.categoryService = ics;
            this.adminLogsActionService = adminLogsActionService;
        }
        //View Page
        public ActionResult CategoryManagement()
        {
            List<CategoryViewModel> categories = this.categoryService.GetCategories().ToList();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditCategory(CategoryViewModel CategoryUpdate)
        {
            if (ModelState.IsValid)
            {
                var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
                categoryService.UpdateCategory(CategoryUpdate);

                var AddActionLogs = new AdminActionLogsViewModel
                {
                    ActionByAdminID = adminId,
                    ActionName = "Edit Category",
                    ActionDateTime = DateTime.Now,
                 

                };

                this.adminLogsActionService.InsertAction(AddActionLogs);
                TempData["EditCategory"] = "Category has been edited successfully.";
                return RedirectToAction("CategoryManagement");
            }else
            {
                ModelState.AddModelError("My Error", "Cannot edit or update this category, please try again.");
            }
            return View(CategoryUpdate);
        }
        //View Page
        public ActionResult EditCategory(int id)
        {
            CategoryViewModel category = this.categoryService.GetCategoryByID(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public ActionResult CreateCategory(CategoryViewModel AddCategory)
        {
            if (ModelState.IsValid)
            {
                var adminId = Convert.ToInt32(Session["CurrentAdminID"]);
                categoryService.InsertCategory(AddCategory);

                var AddActionLogs = new AdminActionLogsViewModel
                {
                    ActionByAdminID = adminId,
                    ActionName = "Add Category",
                    ActionDateTime = DateTime.Now,


                };

                this.adminLogsActionService.InsertAction(AddActionLogs);
                TempData["AddCategory"] = "Category has been added successfully.";
                return RedirectToAction("CategoryManagement");
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot add this category, please try again.");
                return View();
            }


        }
        //View Page
        public ActionResult CreateCategory()
        {
            
            return View();
        }
    }
}