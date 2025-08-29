using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ViewModels;
namespace CodeCommunityFlow.ServiceLayers.ProfilesMapping
{
    public class AnswersMappingProfile : Profile
    {
        public AnswersMappingProfile() {
            CreateMap<Answers, AnswerViewModel>();
            CreateMap<ScoreHistoryLogs, ScoreHistoryViewModel>().ReverseMap();
            CreateMap<DeleteLogs, DeleteLogsViewModel>().ReverseMap();
            CreateMap<Votes, VoteViewModel>();
            CreateMap<newAnswerViewModel, Answers>();
            CreateMap<EditAnswerViewModel, Answers>();
         
        }
    }
}
