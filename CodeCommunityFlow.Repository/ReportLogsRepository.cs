using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.ViewModels;
using System.Data.Entity;

namespace CodeCommunityFlow.Repository
{
    public interface IReportLogsRepository
    {
        void InsertReportLogs(ReportLogs reportLogs);
        List<ReportLogsDTO> GetReportLogs();
      

     }
    public class ReportLogsRepository:IReportLogsRepository
    {
        CodeCommunityFlowDbContext db;

        public ReportLogsRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public List<ReportLogsDTO> GetReportLogs()
        {
            var query = from log in db.ReportLogs
                        join reporter in db.Users on log.ReportedByUserID equals reporter.UserID into reporterGroup
                        from reporter in reporterGroup.DefaultIfEmpty()

                        join reportedUser in db.Users on log.ReportedUserID equals reportedUser.UserID into reportedUserGroup
                        from reportedUser in reportedUserGroup.DefaultIfEmpty()

                        join reportReason in db.Report on log.ReportReasonID equals reportReason.ReportID into reportReasonGroup
                        from reportReason in reportReasonGroup.DefaultIfEmpty()

                        join question in db.Questions on log.QuestionID equals question.QuestionID into questionGroup
                        from question in questionGroup.DefaultIfEmpty()

                        join answer in db.Answers on log.AnswerID equals answer.AnswerID into answerGroup
                        from answer in answerGroup.DefaultIfEmpty()

                        select new ReportLogsDTO
                        {
                            LogID = log.LogID,
                            ReportedTime = log.ReportedTime,
                            ReportType = log.ReportType,

                            Reporter = reporter == null ? null : new UserViewModel
                            {
                                UserID = reporter.UserID,
                                UserName = reporter.UserName,
                                Score = 0,
                                WarningCount = 0,
                                DeletebyAdminCount = 0
                            },
                            ReportedUser = reportedUser == null ? null : new UserViewModel
                            {
                                UserID = reportedUser.UserID,
                                UserName = reportedUser.UserName,
                                Score = reportedUser.Score,
                                WarningCount = reportedUser.WarningCount,
                                DeletebyAdminCount = reportedUser.DeletebyAdminCount
                            },
                            Report = reportReason == null ? null : new ReportViewModel
                            {
                                ReportID = reportReason.ReportID,
                                ReportReason = reportReason.ReportReason,
                                ScoreMinus = reportReason.ScoreMinus
                            },
                            Questions = question == null ? null : new QuestionViewModel
                            {
                                QuestionID = question.QuestionID,
                                QuestionName = question.QuestionName
                            },
                            Answers = answer == null ? null : new AnswerViewModel
                            {
                                AnswerID = answer.AnswerID,
                                AnswerText = answer.AnswerText
                            }
                        };

            var list = query.ToList();
            return list;
        }


        public void InsertReportLogs(ReportLogs reportLogs)
        {
            db.ReportLogs.Add(reportLogs);
            db.SaveChanges();
        }
    }
}
