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
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<Contact, ContactViewModel>().ReverseMap();
            CreateMap<ContactViewModel, Contact>().ReverseMap();
        }
    }
}
