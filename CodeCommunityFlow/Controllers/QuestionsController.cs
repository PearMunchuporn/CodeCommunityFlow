using CodeCommunityFlow.CustomFilter;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModelFiles;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.SelectList;

namespace CodeCommunityFlow.Controllers
{
    public class QuestionsController : Controller
    {
        // GET: Questions
        IQuestionService questionService;
        IAnswerService answerService;
        ICategoryService categoryService;
        IReportService reportService;
        IReportLogsService reportLogsService;
        IUserService userService;
        IDeleteLogsService deleteLogsService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        CodeCommunityFlowDbContext db;

        public QuestionsController(IQuestionService questionService, IAnswerService answerService, ICategoryService categoryService,
            CodeCommunityFlowDbContext db , IReportService reportService, 
            IReportLogsService reportLogsService, IUserService userService, IScoreHistoryLogsService scoreHistoryLogsService,
            IDeleteLogsService deleteLogsService)
        {
            this.questionService = questionService;
            this.answerService = answerService;
            this.categoryService = categoryService;
            this.reportService = reportService;
            this.db = db;
            this.reportLogsService = reportLogsService;
            this.userService = userService;
            this.deleteLogsService = deleteLogsService;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
        }

        //View when click question's page 
        // Order Method
        // 1. View 
        // 2. Create Question [GET]
        // 3. Create Question [POST]
        // 4. DeleteQuestion
        // 5. Edit Question [GET]
        // 6. Edit Question [POST]
        // 7. Report Question 
        public ActionResult View(int id)
        {
            questionService.UpdateQuestionViewCount(id, 1); //count views
            int userID = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel question = questionService.GetQuestionById(id, userID);
           
            var reportReasons = this.reportService.GetReports(); 

            var reportReasonList = reportReasons.Select(r => new SelectListItem
            {
                Value = r.ReportID.ToString(),
                Text = r.ReportReason
            }).ToList();

            var ViewQuestionAnswer = new CodeCommunityFlow.SelectList.SelectReportReason
            {
                QuestionID = question.QuestionID,
                QuestionName = question.QuestionName,
                QuestionContent = question.QuestionContent,
                QuestionDateTime = question.QuestionDateTime,
                UserID = question.UserID,
                CategoryID = question.CategoryID,
                VotesCount = question.VotesCount,
                AnswersCount = question.AnswersCount,
                ViewsCount = question.ViewsCount,
                Image = question.Image,
                ReportReasonSelectList = reportReasonList,
                Answers = question.Answers,
                Categories = question.Categories,
                Users = question.Users,
                CurrentUserVoteType = question.CurrentUserVoteType,
                ReportReason = question.ReportReason,
                Description = question.Description
            };

            return View(ViewQuestionAnswer);

          
        }
    
 

        [AuthorizationAttribute]
        [Route("CreateNewQuestion")]
        public ActionResult CreateQuestion()
        {
            List<CategoryViewModel> categories = categoryService.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [Route("CreateNewQuestion")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AuthorizationAttribute]

        public ActionResult CreateQuestion(AddImgQuestion AddNewQuestion)
        {
            AddNewQuestion.UserID = Convert.ToInt32(Session["CurrentUserID"]);

            AddNewQuestion.QuestionDateTime = DateTime.Now;
            AddNewQuestion.AnswersCount = 0;
            AddNewQuestion.ViewsCount = 0;
            AddNewQuestion.VotesCount = 0;
            
       
            //addImg.ReportID = 0;
       
            if (ModelState.IsValid)
            {

                List<string> img = new List<string>();

                if (AddNewQuestion.Files != null && AddNewQuestion.Files.Any())
                {
                    foreach (var file in AddNewQuestion.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/Questions"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);

                            img.Add("/Uploads/Questions/" + uniqueName);
                        }
                    }
                }


                var NewQuestion = new newQuestionViewModel
                {

                    QuestionContent = AddNewQuestion.QuestionContent,
                    QuestionDateTime = AddNewQuestion.QuestionDateTime,
                    QuestionName = AddNewQuestion.QuestionName,
                    UserID = AddNewQuestion.UserID,
                    VotesCount = AddNewQuestion.VotesCount,
                    AnswersCount = AddNewQuestion.AnswersCount,
                    ViewsCount = AddNewQuestion.ViewsCount,
                    CategoryID = AddNewQuestion.CategoryID,
                    Image = string.Join(";", img),
                  //  ReportID = addImg.ReportID,
             
                };


                questionService.InsertQuestion(NewQuestion);


                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot add the question, please try again.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(int id)
        {
            var question = db.Questions.Where(q => q.QuestionID == id).FirstOrDefault();
            var userOwner = question.UserID;

            var DeleteLogQuestion = new DeleteLogsViewModel
            {
                QuestionID = id,
                AnswerID = null,
                DeleteByAdmin = false,
                UserID = userOwner,
                DeleteDateTime = DateTime.Now,
                DeletionType = "Question"

            };
            this.questionService.DeleteQuestion(id);
            this.deleteLogsService.InsertDeleteLogs(DeleteLogQuestion);
            TempData["SuccessDeleteQuestion"] = "Delete your question successfully.";
            return RedirectToAction("Index", "Home");
        }
        [AuthorizationAttribute]
        [Route("EditQuestion/{id}")]
        public ActionResult EditQuestion(int id)
        {
           var UserId = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel question = this.questionService.GetQuestionById(id, UserId);
            if (question.UserID != UserId)
            {
            
                return RedirectToAction("View", new { id = id });
            }

            return View(question);

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AuthorizationAttribute]
        [Route("EditQuestion")]
        public ActionResult EditQuestion(EditImageQuestion update_Question, string ImgOld, string[] CheckDeleteImage)
        {  
            var user_id = Convert.ToInt32(Session["CurrentUserID"]);
            var Myquestion = this.questionService.GetQuestionById(update_Question.QuestionID, user_id);
            update_Question.QuestionDateTime = DateTime.Now;
            update_Question.ViewsCount = Myquestion.ViewsCount;
            update_Question.VotesCount = Myquestion.VotesCount;

          //  edit_img.ReportID = Myquestion.ReportID;
            var id = Myquestion.QuestionID;
            
            if (ModelState.IsValid)
            {
                List<string> oldPaths = new List<string>();

                if (!string.IsNullOrEmpty(update_Question.ImgOld))
                {
                    oldPaths = update_Question.ImgOld.Split(';').ToList();
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
                if (update_Question.Files != null && update_Question.Files.Any(f => f != null && f.ContentLength > 0))
                {
                    foreach (var file in update_Question.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/Questions"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);

                            newImages.Add("/Uploads/Questions/" + uniqueName);
                        }
                    }
                }
                var allImages = oldPaths.Concat(newImages).ToList();
                var UpdateQuestion = new EditQuestionViewModel
                {
                    Image = string.Join(";", allImages),
                    QuestionContent = update_Question.QuestionContent,
                    QuestionDateTime = update_Question.QuestionDateTime,
                    QuestionName = update_Question.QuestionName,
                    UserID = user_id,
                    VotesCount = update_Question.VotesCount,
                    AnswersCount = update_Question.AnswersCount,
                    ViewsCount = update_Question.ViewsCount,
                    CategoryID = update_Question.CategoryID,
                    QuestionID = update_Question.QuestionID,
                    Description = "Edited Question"


                };


                this.questionService.UpdateQuestionDetail(UpdateQuestion);

                return RedirectToAction("View", "Questions", new { id});
            }

            else
            {
                ModelState.AddModelError("My Error", "Cannot edit the question, please try again.");
                return RedirectToAction("View", new {id });
            }

        }
       


        [AuthorizationAttribute]
        public ActionResult ReportQuestion(int questionId, int reasonId ,int ReportedUserId)
        {
            int ReportedByUserID = Convert.ToInt32(Session["CurrentUserID"]);
            var report = new ReportLogsViewModel
            {
                QuestionID = questionId,
                ReportReasonID = reasonId,
                ReportedByUserID = ReportedByUserID, // who report other users
                ReportedTime = DateTime.Now,
                ReportedUserID = ReportedUserId, // who being reported
                ReportType = "Reported Question"

            };
            var UserGotReportScore = new ScoreHistoryViewModel
            {
                UserID = ReportedUserId,
                ReportReasonID = reasonId,
            };


            this.reportLogsService.InsertReportLogs(report);
            this.userService.UserUpdateScoreWhenGotReport(ReportedUserId, reasonId);
            this.scoreHistoryLogsService.InsertScoreHistory(UserGotReportScore);

            TempData["SuccessReport"] = "Report submitted successfully.";
            return RedirectToAction("View", "Questions", new { id = questionId });
        }
 


    }
}