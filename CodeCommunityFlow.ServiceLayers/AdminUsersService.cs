using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.Repository;
using AutoMapper;

namespace CodeCommunityFlow.ServiceLayers
{
    public interface IAdminUserService
    {
       
        AdminUserViewModels GetAdminUserById(int adminId);
        List<AdminUserViewModels> GetAdminUsers();
        AdminUserViewModels GetAdminUserByEmailandPassword(string Email, string Password);
        void UpdatePasswordAdmin(AdminEditPasswordViewModel updatePassword);
    }
    public class AdminUsersService : IAdminUserService
    {
       IAdminRepository adminUserRepository;
        IMapper _mapper;

        public AdminUsersService(IMapper mapper)
        {
            _mapper = mapper;
            adminUserRepository = new AdminUsersRepository();
        }

        public AdminUserViewModels GetAdminUserByEmailandPassword(string email, string password)
        {
            var admin = adminUserRepository.GetAdminByEmailandPassword(
                email,
                SHA256Generator.GenerateHash(password)
            );

            return admin != null ? _mapper.Map<AdminUserViewModels>(admin) : null;
        }

        public AdminUserViewModels GetAdminUserById(int adminId)
        {
            var admin = adminUserRepository.GetAdminUserById(adminId);
            return admin != null ? _mapper.Map<AdminUserViewModels>(admin) : null;
        }

        public List<AdminUserViewModels> GetAdminUsers()
        {
            var adminUsers = adminUserRepository.GetAdminUsers();
            return _mapper.Map<List<AdminUserViewModels>>(adminUsers);
        }

      

        public void UpdatePasswordAdmin(AdminEditPasswordViewModel updatePassword)
        {
            var PasswordUpdate = _mapper.Map<AdminUsers>(updatePassword);
            PasswordUpdate.Password = SHA256Generator.GenerateHash(updatePassword.Password);
            adminUserRepository.UpdateAdminPassword(PasswordUpdate);
        }
    }

}
