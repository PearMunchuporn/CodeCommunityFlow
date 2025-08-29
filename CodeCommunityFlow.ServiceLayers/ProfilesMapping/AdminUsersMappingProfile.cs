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
    public class AdminUsersMappingProfile:Profile
    {
        public AdminUsersMappingProfile()
        {
            CreateMap<AdminUsers, AdminUserViewModels>();
            CreateMap<AdminUsers, AdminEditPasswordViewModel>().ReverseMap();

        }
    }
}
