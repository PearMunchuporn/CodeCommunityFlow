using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
namespace CodeCommunityFlow.ServiceLayers
{
    public interface IAdminAnnounceService
    {
        void InsertAnnoumentByAdmin(AnnouncementCreateViewModel adminAnnouncement);
        void UpdateAnnoucement(AnnouncementUpdateViewModel adminAnnouncementUpdated);
        AdminAnnouncementViewModels GetAnnouncementByID(int AnnouncementID, int AdminId);
        List<AdminAnnouncementViewModels> GetAnnoucement();
        void UpdateVoteAnnnouncement(int announceId, int value, int adminId);
        void UpdateCommentCount(int AnnounceId, int value);
        void DeleteAnnouncement(int AnnoucementID);
        void UpdateAnnouncementViewCount(int AnnounceID, int value);
    }
    public class AdminAnnouncementService : IAdminAnnounceService
    {
        IAdminAnnouncementRepository announcementRepository;
        IMapper _mapper;

        public AdminAnnouncementService(IMapper mapper)
        {
            _mapper = mapper;
            announcementRepository = new AdminAnnouncementRepository();
        }

        public void DeleteAnnouncement(int AnnoucementID)
        {
            announcementRepository.DeleteAnnouncement(AnnoucementID);
        }

        public List<AdminAnnouncementViewModels> GetAnnoucement()
        {
            var list = announcementRepository.GetAnnoucement().ToList();  
            return _mapper.Map<List<AdminAnnouncementViewModels>>(list);
        }

        public AdminAnnouncementViewModels GetAnnouncementByID(int AnnouncementID, int AdminId = 0)
        {
            var announcement = announcementRepository.GetAnnouncementByID(AnnouncementID);
            if (announcement == null) return null;

            var viewModel = _mapper.Map<AdminAnnouncementViewModels>(announcement);

            foreach (var item in viewModel.CommentFromAnnouncement)
            {
                item.CurrentUserVoteType = 0;
                var vote = item.Votes.FirstOrDefault(v => v.AdminID == AdminId);
                if (vote != null)
                {
                    item.CurrentUserVoteType = vote.VoteValue;
                }
            }

            return viewModel;
        }

        public void InsertAnnoumentByAdmin(AnnouncementCreateViewModel adminAnnouncement)
        {
            var AnnouncementInsert = _mapper.Map<AdminAnnouncement>(adminAnnouncement);
            announcementRepository.InsertAnnoumentByAdmin(AnnouncementInsert);
        }

        public void UpdateAnnoucement(AnnouncementUpdateViewModel adminAnnouncementUpdated)
        {
            var updateAnnounce = _mapper.Map<AdminAnnouncement>(adminAnnouncementUpdated);
            announcementRepository.UpdateAnnoucement(updateAnnounce);
        }

        public void UpdateAnnouncementViewCount(int AnnounceID, int value)
        {
            announcementRepository.UpdateAnnouncementViewCount(AnnounceID, value);
        }

        public void UpdateCommentCount(int AnnounceId, int value)
        {
            announcementRepository.UpdateCommentCount(AnnounceId, value);
        }

        public void UpdateVoteAnnnouncement(int announceId, int value, int adminId)
        {
            announcementRepository.UpdateVoteAnnnouncement(announceId, value, adminId);
        }
    }
}

