using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
using System.Data.Entity;
namespace CodeCommunityFlow.Repository
{
  public interface IAdminAnnouncementRepository
    {
        void InsertAnnoumentByAdmin(AdminAnnouncement adminAnnouncement);
        void UpdateAnnoucement(AdminAnnouncement adminAnnouncementUpdated);
        AdminAnnouncement GetAnnouncementByID(int AnnouncementID);
        IQueryable<AdminAnnouncement> GetAnnoucement();
        void UpdateVoteAnnnouncement(int AnnounceId, int AdminId, int Value);
        void UpdateCommentCount(int AnnounceId , int value);
        void DeleteAnnouncement(int AnnoucementID);
        void UpdateAnnouncementViewCount(int AnnounceID, int value);
    }
   public class AdminAnnouncementRepository: IAdminAnnouncementRepository
    {
        CodeCommunityFlowDbContext db;
        VoteRepository VoteRepository;
        public AdminAnnouncementRepository()
        {
            db = new CodeCommunityFlowDbContext();
            VoteRepository = new VoteRepository();

        }

       public void DeleteAnnouncement(int AnnoucementID)
        {
            AdminAnnouncement adminAnnouncement = db.AdminAnnouncements.Where(a => a.AdminAnnouncementID == AnnoucementID).FirstOrDefault();
            if (adminAnnouncement != null)
            {
                db.AdminAnnouncements.Remove(adminAnnouncement);
         
                db.SaveChanges();
            }
        }

        public IQueryable<AdminAnnouncement> GetAnnoucement()
        {
            return db.AdminAnnouncements;
            /*  .Include(a => a.CommentFromAnnouncement); */// ชื่อ property ให้ถูกต้อง
               // ถ้ามี navigation property นี้
        }

        public AdminAnnouncement GetAnnouncementByID(int AnnouncementID)
        {
            AdminAnnouncement adminAnnouncement = db.AdminAnnouncements.Where(a => a.AdminAnnouncementID == AnnouncementID).FirstOrDefault();
            return adminAnnouncement;
        }

        public void InsertAnnoumentByAdmin(AdminAnnouncement adminAnnouncement)
        {
            db.AdminAnnouncements.Add(adminAnnouncement);
           
            db.SaveChanges();
        }

        public void UpdateAnnoucement(AdminAnnouncement adminAnnouncementUpdated)
        {
            AdminAnnouncement adminAnnouncement = db.AdminAnnouncements
                .Where(a => a.AdminAnnouncementID == adminAnnouncementUpdated.AdminAnnouncementID).FirstOrDefault();

            if (adminAnnouncement != null)
            {
                adminAnnouncement.AdminAnnounceContent = adminAnnouncementUpdated.AdminAnnounceContent;
                adminAnnouncement.AdminAnnounceTopic = adminAnnouncementUpdated.AdminAnnounceTopic;
                adminAnnouncement.ImageContent = adminAnnouncementUpdated.ImageContent;
                adminAnnouncement.VoteCount = adminAnnouncementUpdated.VoteCount;
                adminAnnouncement.CommentCount = adminAnnouncementUpdated.CommentCount;
                adminAnnouncement.AnnouncementDateTime = adminAnnouncementUpdated.AnnouncementDateTime;
                adminAnnouncement.Description = adminAnnouncementUpdated.Description;
                db.SaveChanges();
            }
        }

        public void UpdateAnnouncementViewCount(int AnnounceID, int value )
        {
            AdminAnnouncement announcement = db.AdminAnnouncements.Where(a => a.AdminAnnouncementID == AnnounceID).FirstOrDefault();
            if (announcement != null)
            {
                announcement.ViewCount += 1;
                db.SaveChanges();
            }
        }

        public void UpdateCommentCount(int AnnounceId , int value)
        {
            AdminAnnouncement announcement = db.AdminAnnouncements.Where(a => a.AdminAnnouncementID == AnnounceId).FirstOrDefault();
            if (announcement != null)
            {
                announcement.CommentCount += value;
                db.SaveChanges();

               
            }
        }

        public void UpdateVoteAnnnouncement(int AnnounceId, int Value, int AdminId)
        {
            AdminAnnouncement announcement = db.AdminAnnouncements.Where(a => a.AdminAnnouncementID == AnnounceId).FirstOrDefault();
            if (announcement != null)
            {
                announcement.VoteCount += Value;
                db.SaveChanges();

                VoteRepository.UpdateVoteAnnouncement(AnnounceId, Value, AdminId);
            }
        }
    }
}
