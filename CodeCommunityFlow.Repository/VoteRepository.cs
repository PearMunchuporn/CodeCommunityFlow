using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
namespace CodeCommunityFlow.Repository
{
    public interface IVoteRepository
    {
        void UpdateVoteAnswerCount(int answerId, int value, int userId);
        void UpdateVoteQuestion(int questionId, int userId, int value);
        void UpdateVoteComment(int commentId, int value, int UserID);
        void UpdateVoteAnnouncement(int AnnounceId, int Value, int AdminId);
       


    }
    public class VoteRepository :IVoteRepository
    {
        CodeCommunityFlowDbContext db = new CodeCommunityFlowDbContext();
       public VoteRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public void UpdateVoteAnnouncement(int AnnounceId, int Value, int AdminId)
        {
            int updateValue;
            if (Value > 0) updateValue = 1;
            else if (Value < 0) updateValue = -1;
            else updateValue = 0;
            Votes vote = db.Votes.Where(v => v.AnnounceID == AnnounceId && v.AdminID == AdminId).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Votes newVote = new Votes()
                {
                    AdminID = AdminId,
                    AnnounceID = AnnounceId,
                    VoteValue = updateValue,

                };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        

    }


    //vote answer
    public void UpdateVoteAnswerCount(int answerId, int value, int userId)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            Votes vote = db.Votes.Where(v => v.AnswerID == answerId && v.UserID == userId).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Votes newVote = new Votes()
                {
                    AnswerID = answerId,
                    UserID = userId,
                    VoteValue = updateValue,

                };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        }

        public void UpdateVoteComment(int commentId, int value, int userID)
        {

            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            Votes vote = db.Votes.Where(v => v.CommentID ==commentId && v.UserID == userID).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Votes newVote = new Votes()
                {
                    CommentID = commentId,
                    UserID = userID,
                    VoteValue = updateValue
                };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        }

        //vote question
        public void UpdateVoteQuestion(int QuestionId, int userID, int value)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            Votes vote = db.Votes.Where(v => v.AnswerID == QuestionId && v.UserID == userID).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Votes newVote = new Votes()
                {
                    QuestionID = QuestionId,
                    UserID = userID,
                    VoteValue = updateValue
                };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        }


    }
}
