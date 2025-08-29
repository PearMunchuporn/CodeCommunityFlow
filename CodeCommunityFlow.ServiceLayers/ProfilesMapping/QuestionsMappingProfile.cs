using AutoMapper;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ServiceLayers.ProfilesMapping
{
    public class QuestionsMappingProfile :Profile

    {
        public QuestionsMappingProfile()
        {
            CreateMap<newQuestionViewModel, Questions>();
            CreateMap<EditQuestionViewModel, Questions>();
            CreateMap<ReportLogs, ReportLogsViewModel>();
            CreateMap<Answers, AnswerViewModel>();
            CreateMap<Votes, VoteViewModel>();
            CreateMap<Report, ReportViewModel>();
            CreateMap<ScoreHistoryLogs, ScoreHistoryViewModel>().ReverseMap();
            CreateMap<ThanksLogsViewModel, ThanksLogs>();
            CreateMap<DeleteLogs, DeleteLogsViewModel>().ReverseMap();
            CreateMap<Questions, QuestionViewModel>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
            
        }
    }
}
