using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;


namespace CodeCommunityFlow.Repository
{

   public interface ICommentFromAnnouncementRepository
    {
        void InsertComment(CommentFromAnnouncement comment);
        void UpdateComment(CommentFromAnnouncement commentUpdate);
        void DeleteComment(int CommentID);
        void UpdateCommentVoteCount(int commentId, int value, int userId);
      
        List<CommentFromAnnouncement> GetCommentByAnnouncementId(int AnnouncementId);
        CommentFromAnnouncement GetCommentByCommentId(int commentId);
        List<CommentFromAnnouncement> GetComment();
        List<CommentFromAnnouncement> GetCommentByUserId(int UserId);
    }
    public class CommentFromAnnouncementRepository : ICommentFromAnnouncementRepository
    {
        CodeCommunityFlowDbContext db;
        IVoteRepository voteRepository;
        IAdminAnnouncementRepository AdminAnnouncementRepository;
        public CommentFromAnnouncementRepository()
        {
            db = new CodeCommunityFlowDbContext();
            voteRepository = new VoteRepository();
            AdminAnnouncementRepository = new AdminAnnouncementRepository();
        }
        public void DeleteComment(int CommentID)
        {
            CommentFromAnnouncement comment = db.CommentFromAnnouncement.Where(a => a.CommentID == CommentID).FirstOrDefault();
            if (comment != null)
            {
                var relatedLogs = db.ReportLogs.Where(r => r.CommentID == CommentID).ToList();
                foreach (var log in relatedLogs)
                {
                    log.CommentID = null;
                }


                db.CommentFromAnnouncement.Remove(comment);

                db.SaveChanges();
                AdminAnnouncementRepository.UpdateCommentCount(comment.AdminAnnouncementID, -1);
            }
        }

        public List<CommentFromAnnouncement> GetComment()
        {
            List<CommentFromAnnouncement> comments = db.CommentFromAnnouncement.ToList();
            return comments;
        }

        public List<CommentFromAnnouncement> GetCommentByAnnouncementId(int AnnouncementId)
        {
            List<CommentFromAnnouncement> comments = db.CommentFromAnnouncement.Where(c=>c.AdminAnnouncementID==AnnouncementId).ToList();
            return comments;
        }

        public CommentFromAnnouncement GetCommentByCommentId(int commentId)
        {
            CommentFromAnnouncement comment = db.CommentFromAnnouncement.Where(c => c.CommentID == commentId).FirstOrDefault();
            return comment;
        }

        public List<CommentFromAnnouncement> GetCommentByUserId(int UserId)
        {
            List<CommentFromAnnouncement> comment = db.CommentFromAnnouncement.Where(c => c.UserID == UserId).ToList();
            return comment;
        }

        public void InsertComment(CommentFromAnnouncement comment)
        {
            db.CommentFromAnnouncement.Add(comment);
            db.SaveChanges();
        }

        public void UpdateComment(CommentFromAnnouncement commentUpdate)
        {
            CommentFromAnnouncement comment = db.CommentFromAnnouncement.Where(c => c.CommentID == commentUpdate.CommentID).FirstOrDefault();
            if(comment!=null)
            {
                comment.CommentContent = commentUpdate.CommentContent;
                comment.Image = commentUpdate.Image;
                comment.VoteCount = commentUpdate.VoteCount;
                comment.AdminAnnouncementID = commentUpdate.AdminAnnouncementID;
                comment.UserID = commentUpdate.UserID;
                comment.Description = commentUpdate.Description;
                db.SaveChanges();
            }
        }

     

        public void UpdateCommentVoteCount(int commentId, int value, int userId)
        {
            CommentFromAnnouncement comment = db.CommentFromAnnouncement.Where(q => q.CommentID == commentId).FirstOrDefault();
            if (comment != null)
            {
                comment.VoteCount += value;
                voteRepository.UpdateVoteComment(commentId,value,userId);
                db.SaveChanges();
            }
        }
    }
}
