using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.Repository
{
    public interface IContactRepository
    {
        void InsertContact(Contact contactUs);
        List<Contact> GetContact();
        Contact GetContactorByEmail(string Email);
      
    }
    public class ContactRepository : IContactRepository
    {
        CodeCommunityFlowDbContext db;

        public ContactRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public List<Contact> GetContact()
        {
            List<Contact> contacts = db.ContactUS.ToList();
            return contacts;
        }

        public Contact GetContactorByEmail(string Email)
        {
            Contact contact = db.ContactUS.Where(c => c.WorkEmail == Email).FirstOrDefault();
            return contact;
        }

        public void InsertContact(Contact contactUs)
        {
            db.ContactUS.Add(contactUs);
            db.SaveChanges();
        }
    }
}
