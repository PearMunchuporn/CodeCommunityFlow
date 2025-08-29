using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace CodeCommunityFlow.Repository
{

    public interface IQuestionRepository
    {
        void InsertQuestion(Questions questions);
        Questions GetQuestionById(int questionId);
        void UpdateQuestionDetail(Questions questions);
        void UpdateQuestionVoteCount(int questionId,int userId, int value);
        void UpdateQuestionAnswerCount(int questionId, int value);
        void UpdateQuestionViewCount(int questionId, int value);
        void DeleteQuestion(int questionID);
        List<Questions> GetQuestions();
        List<Questions> GetQuestionByUser(int UserID);

    }

    public class QuestionRepository:IQuestionRepository
    {
        CodeCommunityFlowDbContext db;
        IVoteRepository voteRepository;

        public QuestionRepository()
        {
            db = new CodeCommunityFlowDbContext();
            voteRepository = new VoteRepository();
        }
        public List<Questions> GetQuestions()
        {
            var AllQuestions = db.Questions.ToList();
            return AllQuestions;
        }



        public void InsertQuestion(Questions questions)
        {
            db.Questions.Add(questions);
            db.SaveChanges();
        }

        public Questions GetQuestionById(int questionId)
        {
            Questions question = db.Questions
         //.Include(q => q.ReportLogs.Select(r => r.ReportReason)) // include nested
         //.Include(q => q.ReportLogs.Select(r => r.ReportedByUser))
         //.Include(q => q.ReportLogs.Select(r => r.ReportedUser))
         .Where(q => q.QuestionID == questionId)
         .FirstOrDefault();
          return question;

        }
        public void UpdateQuestionDetail(Questions questionUpdate)
        {
            Questions question = db.Questions.Where(q => q.QuestionID == questionUpdate.QuestionID).FirstOrDefault();
            if (question != null)
            {
                question.QuestionName = questionUpdate.QuestionName;
                question.QuestionContent = questionUpdate.QuestionContent;
                question.QuestionDateTime = DateTime.Now;
                question.Image = questionUpdate.Image;
                question.CategoryID = questionUpdate.CategoryID;
                question.ViewsCount = questionUpdate.ViewsCount;
                question.VotesCount = questionUpdate.VotesCount;
                question.Description = questionUpdate.Description;
               
               // question.ReportID = questionUpdate.ReportID;
                db.SaveChanges();
            }

        }

        public void UpdateQuestionViewCount(int questionId, int value)
        {
            Questions question = db.Questions.Where(q => q.QuestionID == questionId).FirstOrDefault();
            if (question != null)
            {
                question.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionAnswerCount(int questionId, int value)
        {
            Questions question = db.Questions.Where(q => q.QuestionID == questionId).FirstOrDefault();
            if (question != null)
            {
                question.AnswersCount += value;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionVoteCount(int questionId, int userId ,int value)
        {
            Questions question = db.Questions.Where(q => q.QuestionID == questionId).FirstOrDefault();
            if (question != null)
            {
                question.VotesCount += value;
                voteRepository.UpdateVoteQuestion(questionId, userId, value);
                db.SaveChanges();
            }
        }

        public void DeleteQuestion(int questionId)
        {
            Questions deleteQuestion = db.Questions.Where(q => q.QuestionID == questionId).FirstOrDefault();
            if (deleteQuestion != null)
            {
                db.Questions.Remove(deleteQuestion);
                db.SaveChanges();
            }
        }

        public List<Questions> GetQuestionByUser(int UserId)
        {
            List<Questions> questions = db.Questions.Where(q => q.UserID == UserId).ToList();
            return questions;

        }

       
    }
}
