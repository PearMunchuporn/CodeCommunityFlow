using CodeCommunityFlow.CustomFilter;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodeCommunityFlow.Controllers
{
   
   
    public class ContactUsController : Controller
    {
        // GET: Contact
        IContactService contactService;
        IHowknowService howknowService;

        public ContactUsController(IContactService icon, IHowknowService ihks)
        {
         
            this.contactService = icon;
            this.howknowService = ihks;
        }
        [Route("ContactUs")]
        private async Task<List<SelectListItem>> GetCountriesAsync()
        {
            var countries = new List<SelectListItem>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://restcountries.com/");
                var response = await client.GetAsync("v3.1/all?fields=name,cca2");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JArray.Parse(json);

                    foreach (var country in data)
                    {
                        var name = country["name"]?["common"]?.ToString();
                        var code = country["cca2"]?.ToString();

                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(code))
                        {
                            countries.Add(new SelectListItem { Text = name, Value = name });
                        }
                    }
                }
            }

            return countries.OrderBy(c => c.Text).ToList();
        }


        [HttpGet]
        [Route("ContactUs")]
        public async Task<ActionResult> ContactUs()
        {
            var model = new CountryViewModel();

            ViewBag.HowKnowUs = howknowService.GetHowKnowUs();
            ViewBag.CountryList = await GetCountriesAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ContactUs")]
        public async Task<ActionResult> InsertContactUs(CountryViewModel contact)
        {
            ViewBag.HowKnowUs = howknowService.GetHowKnowUs();
            ViewBag.CountryList = await GetCountriesAsync();

            if (ModelState.IsValid)
            {

                var FormView = new ContactViewModel
                {

                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Company = contact.Company,
                    Country = contact.Country,
                    HowHearUsID = contact.HowHearUsID,
                    WorkEmail = contact.WorkEmail
                };
                this.contactService.InsertContact(FormView);
                TempData["SuccessContact"] = "Thanks you for contact us.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("My Error", "Please try again.");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}