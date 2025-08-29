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
   public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Users, UserViewModel>();
            CreateMap<Users, EditUserDetailsViewModel>().ReverseMap();
            CreateMap<Users, EditUserPasswordViewModel>().ReverseMap();
            CreateMap<Users, WarnUserViewModel>().ReverseMap();
            CreateMap<Users, UpdateUserScoreViewModel>().ReverseMap();
            CreateMap<Users, RegisterViewModel>().ReverseMap();
            
         
        }
    }
}
