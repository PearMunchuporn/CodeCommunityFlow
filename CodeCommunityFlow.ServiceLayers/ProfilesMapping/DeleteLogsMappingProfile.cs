using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ViewModels;
namespace CodeCommunityFlow.ServiceLayers.ProfilesMapping
{
    public class DeleteLogsMappingProfile:Profile
    {
        public DeleteLogsMappingProfile()
        {

            CreateMap<DeleteLogs, DeleteLogsViewModel>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question == null ? null : new QuestionViewModel
                {
                    QuestionID = src.Question.QuestionID,
                    QuestionName = src.Question.QuestionName
                }))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer == null ? null : new AnswerViewModel
                {
                    AnswerID = src.Answer.AnswerID,
                    AnswerText = src.Answer.AnswerText
                }))
                .ForMember(dest => dest.UserDeletion, opt => opt.MapFrom(src => src.UserDeletion == null ? null : new UserViewModel
                {
                    UserID = src.UserDeletion.UserID,
                    UserName = src.UserDeletion.UserName,
                    Score = src.UserDeletion.Score,
                    DeletebyAdminCount = src.UserDeletion.DeletebyAdminCount
                }))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.CommentFromAnnoucement == null ? null : new CommentFromAnnouncementViewModel
                {
                    CommentID = src.CommentFromAnnoucement.CommentID,
                    CommentContent = src.CommentFromAnnoucement.CommentContent
                }))
                .ForMember(dest => dest.DeleteLogsID, opt => opt.MapFrom(src => src.DeleteLogsID))
                .ForMember(dest => dest.QuestionID, opt => opt.MapFrom(src => src.QuestionID))
                .ForMember(dest => dest.AnswerID, opt => opt.MapFrom(src => src.AnswerID))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.DeleteDateTime, opt => opt.MapFrom(src => src.DeleteDateTime))
                .ForMember(dest => dest.DeleteByAdmin, opt => opt.MapFrom(src => src.DeleteByAdmin))
                .ForMember(dest => dest.CommentID, opt => opt.MapFrom(src => src.CommentID))
                .ForMember(dest => dest.DeletionType, opt => opt.MapFrom(src => src.DeletionType));
                    
            
        }
    }
}
