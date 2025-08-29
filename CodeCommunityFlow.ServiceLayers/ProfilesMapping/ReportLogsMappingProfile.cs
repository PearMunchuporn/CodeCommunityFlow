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
    public class ReportLogsMappingProfile:Profile
    {
        public ReportLogsMappingProfile()
        {
        
                CreateMap<ReportLogs, ReportLogsViewModel>()
                    .ForMember(dest => dest.Reporter, opt => opt.MapFrom(src => src.ReportedByUser))
                    .ForMember(dest => dest.ReportedUser, opt => opt.MapFrom(src => src.ReportedUser))
                    .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.ReportReason))
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Question))
                    .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answer));

                CreateMap<Users, UserViewModel>();
                CreateMap<Report, ReportViewModel>();
                CreateMap<Questions, QuestionViewModel>();
                CreateMap<Answers, AnswerViewModel>();
                CreateMap<ReportLogsDTO, ReportLogsViewModel>();


        }
    
    }
}

