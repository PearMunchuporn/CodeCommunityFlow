using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.DomainModels;

namespace CodeCommunityFlow.ServiceLayers.ProfilesMapping
{
    public class AdminAnnouncementMappingProfile:Profile
    {
        public AdminAnnouncementMappingProfile()
        {
            CreateMap<AdminAnnouncement, AdminAnnouncementViewModels>().ForMember(dest => dest.CommentFromAnnouncement, opt => opt.MapFrom(src => src.CommentFromAnnouncement));
            CreateMap<AdminUsers, AdminUserViewModels>();
            CreateMap<Votes, VoteViewModel>();
            CreateMap<AnnouncementCreateViewModel, AdminAnnouncement>();
            CreateMap<AnnouncementUpdateViewModel, AdminAnnouncement>();
            CreateMap<CommentFromAnnouncement, CommentFromAnnouncementViewModel>().ReverseMap();
            CreateMap<ScoreHistoryLogs, ScoreHistoryViewModel>().ReverseMap();
            CreateMap<DeleteLogs, DeleteLogsViewModel>().ReverseMap();


        }
    }
}
