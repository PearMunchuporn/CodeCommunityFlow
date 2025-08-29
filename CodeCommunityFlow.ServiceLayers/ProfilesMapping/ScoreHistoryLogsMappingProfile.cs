using AutoMapper;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ViewModels;

namespace CodeCommunityFlow.ServiceLayers.ProfilesMapping
{
    public class ScoreHistoryLogsMappingProfile : Profile
    {
        public ScoreHistoryLogsMappingProfile()
        {
     
            CreateMap<Users, UserViewModel>();
            CreateMap<Report, ReportViewModel>();


            CreateMap<ScoreHistoryLogs, ScoreHistoryViewModel>()
                .ForMember(scoreLogs => scoreLogs.UserScore, opt => opt.MapFrom(src => src.UserScore))
                .ForMember(scoreLogs => scoreLogs.ReasonReportScore, opt => opt.MapFrom(src => src.ReasonReportScore));

         
               
        }
    }
}
