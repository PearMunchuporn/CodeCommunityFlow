using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;

namespace CodeCommunityFlow.Repository
{
    public interface IAdminRepository
    {
         List<AdminUsers> GetAdminUsers();
     
        AdminUsers GetAdminByEmailandPassword(string Email, string Password);
        AdminUsers GetAdminUserById(int AdminId);
        void UpdateAdminPassword(AdminUsers PasswordUpdated);
    }
    public class AdminUsersRepository : IAdminRepository
    {
        CodeCommunityFlowDbContext db;

        public AdminUsersRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public AdminUsers GetAdminByEmailandPassword(string Email, string Password)
        {
            AdminUsers ThisAdmin = db.AdminUsers.Where(a => a.Email == Email && a.Password == Password).FirstOrDefault();
            return ThisAdmin;
        }

        public AdminUsers GetAdminUserById(int AdminId)
        {
            AdminUsers ThisAdmin = db.AdminUsers.Where(a => a.AdminID== AdminId).FirstOrDefault();
            return ThisAdmin;
        }

        public List<AdminUsers> GetAdminUsers()
        {
            List<AdminUsers> adminUsers = db.AdminUsers.ToList();
            return adminUsers;
        }

     
        public void UpdateAdminPassword(AdminUsers PasswordUpdated)
        {
            AdminUsers adminUsers = db.AdminUsers.Where(a => a.AdminID == PasswordUpdated.AdminID).FirstOrDefault();
            if(adminUsers!=null)
            {
                adminUsers.Password = PasswordUpdated.Password;
                db.SaveChanges();

            }
        }
    }
}
