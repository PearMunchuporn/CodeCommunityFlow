using CodeCommunityFlow.CustomFilter;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModelFiles;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace CodeCommunityFlow.Controllers
{
    public class AnswersController : Controller
    {
        // GET: Answers
        IAnswerService answerService;
        IUserService userService;
        IDeleteLogsService deleteLogsService;
        CodeCommunityFlowDbContext db;
        IReportLogsService reportLogsService;
        IQuestionService questionService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        IThankLogsService thankLogsService;
        public AnswersController(IAnswerService answerService, IUserService userService, CodeCommunityFlowDbContext db , IDeleteLogsService DeleteLogsService, 
            IReportLogsService ReportLogsService, IQuestionService QuestionService, IScoreHistoryLogsService scoreHistoryLogsService,
              IThankLogsService thankLogsService)
        {
            this.answerService = answerService;
            this.userService = userService;
            this.db = db;
            this.deleteLogsService = DeleteLogsService;
            this.reportLogsService = ReportLogsService;
            this.questionService = QuestionService;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
            this.thankLogsService = thankLogsService;
        }
        public ActionResult Index()
        {
            return View();
        }
        //Order Methode
        // 1. Add Answer
        // 2. Edit Answer
        // 3. Delete Answer
        // 4. Report Answer
        // 5. Thank To This Answer

        //----------------------------Add Answer--------Add Answer-------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorization]
        [Route("AddAnswer")]
        public ActionResult AddAnswer(NewAnswerViewModelFiles addNewAnswer)
        {
            addNewAnswer.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            addNewAnswer.AnswerDateTime = DateTime.Now;
            addNewAnswer.VotesCount = 0;


            if (ModelState.IsValid)
            {
                List<string> img = new List<string>();

                if (addNewAnswer.Files != null && addNewAnswer.Files.Any())
                {
                    foreach (var file in addNewAnswer.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/Answers"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);

                            img.Add("/Uploads/Answers/" + uniqueName);
                        }
                    }
                }

                var InsertNewAns = new newAnswerViewModel
                {
                    AnswerText = addNewAnswer.AnswerText,
                    AnswerDateTime = addNewAnswer.AnswerDateTime,
                    QuestionID = addNewAnswer.QuestionID,
                    UserID = addNewAnswer.UserID,
                    VotesCount = addNewAnswer.VotesCount,
                    Image = string.Join(";", img),

                };

                var scoreAddAnswerLogs = new ScoreHistoryViewModel
                {
                    UserID = addNewAnswer.UserID,
                    AddAnswer = +10

                };
               
                answerService.InsertAnswer(InsertNewAns);
                userService.UpdateScoreWhenAnswerQuestion(addNewAnswer.UserID);
                scoreHistoryLogsService.InsertScoreHistory(scoreAddAnswerLogs);

                return RedirectToAction("View", "Questions", new { id = addNewAnswer.QuestionID });
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot answer the question, please try again.");

                var questionViewModel = questionService.GetQuestionById(addNewAnswer.QuestionID, addNewAnswer.UserID);
                return View("View", "Questions", questionViewModel);
            }
        }
        //--------------------------Edit Answer-----------------Edit Answer-------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorization]
        [Route("UpdateAnswer")]
        public ActionResult EditAnswer(EditImageAnswerViewModel edit_answer, string ImageOld, string[] CheckDeleteImage = null)
        {

            var answer = this.answerService.GetAnswersByAnswerId(edit_answer.AnswerID);
            edit_answer.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            edit_answer.AnswerDateTime = DateTime.Now;
            edit_answer.VotesCount = answer.VotesCount;

            if (ModelState.IsValid)
            {

                List<string> oldPaths = new List<string>();

                if (!string.IsNullOrEmpty(edit_answer.ImageOld))
                {
                    oldPaths = edit_answer.ImageOld.Split(';').ToList();
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

                if (edit_answer.Files != null && edit_answer.Files.Any(f => f != null && f.ContentLength > 0))
                {
                    foreach (var file in edit_answer.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uniqueName = "_" + DateTime.Now.Ticks + fileName;
                            var path = Path.Combine(Server.MapPath("~/Uploads/Answers"), uniqueName);

                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            file.SaveAs(path);

                            newImgs.Add("/Uploads/Answers/" + uniqueName);
                        }
                    }
                }

                var allImages = oldPaths.Concat(newImgs).ToList();
                var UpdateAns = new EditAnswerViewModel
                {
                    AnswerID = edit_answer.AnswerID,
                    AnswerText = edit_answer.AnswerText,
                    AnswerDateTime = edit_answer.AnswerDateTime,
                    QuestionID = edit_answer.QuestionID,
                    UserID = edit_answer.UserID,
                    VotesCount = edit_answer.VotesCount,
                    Image = string.Join(";", allImages),
                    Description = "Edited Answer"
                };

                var ScoreUpdateAnswerlogs = new ScoreHistoryViewModel
                {
                    UserID = edit_answer.UserID,
                    UpdateAnswer = +5,
                };

                answerService.UpdateAnswer(UpdateAns);
                userService.UpdateScoreWhenUserUpdateAnswer(edit_answer.UserID);
                scoreHistoryLogsService.InsertScoreHistory(ScoreUpdateAnswerlogs);
                return RedirectToAction("View", "Questions", new { id = edit_answer.QuestionID });
            }
            else
            {
                ModelState.AddModelError("My Error", "Cannot answer the question, please try again.");



                return RedirectToAction("View" , "Questions", new { id = edit_answer.QuestionID });
            }
        }

        //--------------------------Delete Answer------------------------Delete Answer------------------------------------------

        [AuthorizationAttribute]
        public ActionResult DeleteAnswer(int id)
        {

            var answer = db.Answers.Where(a => a.AnswerID == id).FirstOrDefault();
            int questionId = answer.QuestionID;

            int userId = answer.UserID.Value;

            var deleteLogAnswer = new DeleteLogsViewModel
            {
                AnswerID = id,
                QuestionID = questionId,
                UserID = userId,
                DeleteDateTime = DateTime.Now,
                DeleteByAdmin = false,
                DeletionType = "Answer"

            };

            this.answerService.DeleteAnswer(id);
            this.deleteLogsService.InsertDeleteLogs(deleteLogAnswer);
            TempData["SuccessDeleteAnswer"] = "Delete your answer successfully.";

            return RedirectToAction("View", "Questions", new { id = questionId });
        }

        ///-------------------------------Report   Answer------------------------------Report   Answer-----------
        [AuthorizationAttribute]
        public ActionResult ReportAnswer(int answerId, int reasonId, int questionId, int reportedUserId)
        {
            int ReportedByUserID = Convert.ToInt32(Session["CurrentUserID"]);
            var report = new ReportLogsViewModel
            {
                AnswerID = answerId,
              //  QuestionID = questionId,
                ReportReasonID = reasonId,
                ReportedByUserID = ReportedByUserID, // who is reporting
                ReportedUserID = reportedUserId, // who being reported
                ReportedTime = DateTime.Now,
                ReportType = "Reported Answer"
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
            return RedirectToAction("View", "Questions", new { id = questionId });
        }
        [HttpPost]
        public JsonResult ThankToThisAnswer(int userId ,int answerId )
        {
            var userThanks = Convert.ToInt32(Session["CurrentUserID"]);
            this.userService.UpdateScoreWhenUserGotThankTo(userId);
            this.answerService.ThankToAnswer(answerId);
            var ScoreUpdateWhenGotThankToLogs = new ScoreHistoryViewModel
            {
                UserID = userId,
                GotThankTo = 30
            };
            this.scoreHistoryLogsService.InsertScoreHistory(ScoreUpdateWhenGotThankToLogs);
     
            var thanksToThisAns = new ThanksLogsViewModel
            {
                ThankedUserID = userId,
                UserIDGaveThank = userThanks,
                ThankedAnswerID = answerId,

            };
            this.thankLogsService.InsertThankLogs(thanksToThisAns);
            TempData["SuccessThankTo"] = "Thank To user successfully.";
            return Json(new
            {
                success = true,
                userId = userId,
                answerId = answerId
            
            });


        }
    }
}