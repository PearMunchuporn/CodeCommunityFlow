using AutoMapper;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.Repository;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ServiceLayers
{
    public interface IContactService
    {
        List<ContactViewModel> GetContact();
        void InsertContact(ContactViewModel contact);
        ContactViewModel GetContactorByEmail(string Email);



    }
    public class ContactService : IContactService
    {
        IContactRepository contactRepository;
        IMapper _mapper;
        public ContactService(IMapper mapper)
        {
            contactRepository = new ContactRepository();
            _mapper = mapper;
        }

    

        public List<ContactViewModel> GetContact()
        {
            var AllContactors = contactRepository.GetContact().ToList();
            return _mapper.Map<List<ContactViewModel>>(AllContactors);
        }

        public ContactViewModel GetContactorByEmail(string Email)
        {
            var contactor = contactRepository.GetContactorByEmail(Email);
            return _mapper.Map<ContactViewModel>(contactor);
        }

        public void InsertContact(ContactViewModel contactInsert)
        {
            var Insertcontact = _mapper.Map<Contact>(contactInsert);
            contactRepository.InsertContact(Insertcontact);
        }
    }
}
