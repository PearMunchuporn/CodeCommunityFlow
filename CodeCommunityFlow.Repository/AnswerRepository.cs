using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
namespace CodeCommunityFlow.Repository
{
    public interface IAnswerRepository
    {
        void InsertAnswer(Answers ans);
        void UpdateAnswer(Answers ans);
        void DeleteAnswer(int AnswerID);
        void UpdateAnswerVoteCount(int answerId, int value, int userId);
        void ThankToAnswer(int AnswerID);
        List<Answers> GetAnswersByQuestionId(int QuestionID);
        List<Answers> GetAnswersByAnswerId(int AnswerID);
        List<Answers> GetAnswers();
        List<Answers> GetAnswerByUserId(int UserId);
   
    }
    public class AnswerRepository :IAnswerRepository
    {
        CodeCommunityFlowDbContext db;
        IQuestionRepository questionRepository;
        IVoteRepository voteRepository;
        public AnswerRepository()
        {
            db = new CodeCommunityFlowDbContext();
            questionRepository = new QuestionRepository();
            voteRepository = new VoteRepository();
        }

        public List<Answers> GetAnswersByAnswerId(int AnswerID)
        {
            List<Answers> answers = db.Answers.Where(a => a.AnswerID == AnswerID).ToList();
            return answers;
        }

        public List<Answers> GetAnswersByQuestionId(int QuestionID)
        {
            List<Answers> ans = db.Answers.Where(a => a.QuestionID == QuestionID).OrderByDescending(a => a.AnswerDateTime).ToList();
            return ans;
        }

        public void InsertAnswer(Answers ans)
        {
            db.Answers.Add(ans);
            db.SaveChanges();
            questionRepository.UpdateQuestionAnswerCount(ans.QuestionID, 1);
        }

        public void UpdateAnswer(Answers ansUpdate)
        {
            Answers answer = db.Answers.Where(a => a.AnswerID == ansUpdate.AnswerID).FirstOrDefault();
            if (answer != null)
            {
                answer.AnswerDateTime = ansUpdate.AnswerDateTime;
                answer.AnswerText = ansUpdate.AnswerText;
                answer.Image = ansUpdate.Image;
                answer.UserID = ansUpdate.UserID;
                answer.QuestionID = ansUpdate.QuestionID;
                answer.VotesCount = ansUpdate.VotesCount;
                answer.Description = ansUpdate.Description;
                db.SaveChanges();
            }
        }


        public void UpdateAnswerVoteCount(int answerId, int value, int userId)
        {
            Answers answer = db.Answers.Where(a => a.AnswerID == answerId).FirstOrDefault();
            if (answer != null)
            {
                answer.VotesCount += value;
                db.SaveChanges();
             
                voteRepository.UpdateVoteAnswerCount(answerId, value, userId);
            }
        }

    

        public void DeleteAnswer(int answerID)
        {
            Answers answer = db.Answers.Where(a => a.AnswerID == answerID).FirstOrDefault();
            if (answer != null)
            {
                var relatedLogs = db.ReportLogs.Where(r => r.AnswerID == answerID).ToList();
                foreach (var log in relatedLogs)
                {
                    log.AnswerID = null;
                }

          
                db.Answers.Remove(answer);         

                db.SaveChanges();
                questionRepository.UpdateQuestionAnswerCount(answer.QuestionID, -1);
            }

        }

        public List<Answers> GetAnswers()
        {
            List<Answers> answers = db.Answers.ToList();
            return answers;
        }

        public List<Answers> GetAnswerByUserId(int UserId)
        {
            List<Answers> answer = db.Answers.Where(u => u.UserID == UserId).ToList();
            return answer;
        }

        public void ThankToAnswer(int AnswerID)
        {
            Answers answers = db.Answers.Where(a => a.AnswerID == AnswerID).FirstOrDefault();
            if (answers != null)
            {
                answers.isThankedTo = true;
                db.SaveChanges();
            }
        }
    }
}

