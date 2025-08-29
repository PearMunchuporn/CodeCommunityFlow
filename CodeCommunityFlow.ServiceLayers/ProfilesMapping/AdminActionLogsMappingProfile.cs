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
    public class AdminActionLogsMappingProfile:Profile
    {
        public AdminActionLogsMappingProfile()
        {
            CreateMap<AdminActionLogs, AdminActionLogsViewModel>().ReverseMap(); 
            CreateMap<Users, UserViewModel>();
            CreateMap<AdminUsers, AdminUserViewModels>();
            
        }
    }
}
