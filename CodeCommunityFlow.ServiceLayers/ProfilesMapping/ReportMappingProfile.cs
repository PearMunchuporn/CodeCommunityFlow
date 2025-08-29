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
    public class ReportMappingProfile:Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<Report, ReportViewModel>().ReverseMap();
            CreateMap<ReportLogs, ReportLogsViewModel>().ReverseMap();
        }
    }
}
