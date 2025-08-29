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
    public class ViewQuestionAndAnswerController : Controller
    {
        // GET: Admin/ViewQuestionAndAnswer
        IQuestionService questionService;
   
      

        public ViewQuestionAndAnswerController(IQuestionService iqs)
        {
            this.questionService = iqs;
            
        }
        public ActionResult ViewByAdmin(int id)
        {
            questionService.UpdateQuestionViewCount(id, 0); //count views
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel question = questionService.GetQuestionById(id, uid);
           
            return View(question);
        }
    }
}