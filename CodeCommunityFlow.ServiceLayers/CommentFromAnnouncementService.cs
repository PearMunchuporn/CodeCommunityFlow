using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.DomainModels;
using System.Threading.Tasks;
using CodeCommunityFlow.Repository;
using AutoMapper;

namespace CodeCommunityFlow.ServiceLayers
{

   public interface ICommentFromAnnouncementService
    {
        void InsertComment(AddCommentViewModel comment);
        void UpdateComment(UpdateCommentViewModel commentUpdate);
        void DeleteComment(int CommentID);
        void UpdateCommentVoteCount(int commentId, int value, int userId);
        void DeleteCommentByAdmin(int CommentID, int UserID);

        List<CommentFromAnnouncementViewModel> GetCommentByAnnouncementId(int AnnouncementId);
        CommentFromAnnouncementViewModel GetCommentByCommentId(int commentId);
        List<CommentFromAnnouncementViewModel> GetComment();
        List<CommentFromAnnouncementViewModel> GetCommentByUserId(int UserId);
    }
   public class CommentFromAnnouncementService: ICommentFromAnnouncementService
    {

        ICommentFromAnnouncementRepository commentRepository;
        IMapper _mapper;
        IUserRepository userRepository;
        public CommentFromAnnouncementService(IMapper mapper)
        {
            commentRepository = new CommentFromAnnouncementRepository();
            _mapper = mapper;
            userRepository = new UserRepository();
        }

        public void DeleteComment(int CommentID)
        {
            commentRepository.DeleteComment(CommentID);
        }

        public List<CommentFromAnnouncementViewModel> GetComment()
        {
            var AllComments = commentRepository.GetComment();
            return _mapper.Map<List<CommentFromAnnouncementViewModel>>(AllComments);
        }

        public List<CommentFromAnnouncementViewModel> GetCommentByAnnouncementId(int AnnouncementId)
        {
            var comments = commentRepository.GetCommentByAnnouncementId(AnnouncementId).ToList();
            return _mapper.Map<List<CommentFromAnnouncementViewModel>>(comments);

        }

        public CommentFromAnnouncementViewModel GetCommentByCommentId(int commentId)
        {
            var comment = commentRepository.GetCommentByCommentId(commentId);
            return _mapper.Map<CommentFromAnnouncementViewModel>(comment);
        }

        public List<CommentFromAnnouncementViewModel> GetCommentByUserId(int UserId)
        {
            var comments = commentRepository.GetCommentByUserId(UserId).ToList();
            return _mapper.Map<List<CommentFromAnnouncementViewModel>>(comments);
        }
        
        public void InsertComment(AddCommentViewModel comment)
        {
            var commentInsert = _mapper.Map<CommentFromAnnouncement>(comment);
            commentRepository.InsertComment(commentInsert);
        }

        public void UpdateComment(UpdateCommentViewModel commentUpdate)
        {
            var Updatecomment = _mapper.Map<CommentFromAnnouncement>(commentUpdate);
            commentRepository.UpdateComment(Updatecomment);
        }

        public void UpdateCommentVoteCount(int commentId, int value, int userId)
        {
            commentRepository.UpdateCommentVoteCount(commentId, value, userId);
        }
        public void DeleteCommentByAdmin(int CommentID, int UserID)
        {
            commentRepository.DeleteComment(CommentID);
            userRepository.UpdateUserScoreWhenAdminDelete(UserID);

        }
    }
}
