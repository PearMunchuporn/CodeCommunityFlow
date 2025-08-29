using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
namespace CodeCommunityFlow.Areas.Admin.Controllers
{
    public class ContactorsListController : Controller
    {
        // GET: Admin/ContactList
        IContactService contactService;
        public ContactorsListController(IContactService iconser)
        {
            this.contactService = iconser;
        }

        public ActionResult ContactorsList()
        {

           
            List<ContactViewModel> contacts= this.contactService.GetContact().ToList();
            return View(contacts);
        }

        
    }
}