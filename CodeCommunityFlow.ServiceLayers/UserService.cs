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
    public interface IUserService
    {
        int InsertUser(RegisterViewModel user);
        void UpdateUserDetails(EditUserDetailsViewModel EditUserDetails);
        void UpdateUserPassword(EditUserPasswordViewModel EditUserPassword);
        void DeleteUser(int userId);
        List<UserViewModel> GetUsers();
        UserViewModel GetUserByEmailAndPassword(string Email, string Password);
        UserViewModel GetUserByEmail(string Email);
        UserViewModel GetUserByUserId(int userId);
        void UpdateScoreWhenAnswerQuestion(int userId);
        void UpdateScoreWhenUserUpdateAnswer(int userId);
        void InsertMsgToUser(WarnUserViewModel userMsg);
        void UserUpdateScoreWhenGotReport(int reportedUserId, int reasonId);
        void UpdateScoreWhenUserGotWarning(int userId);
        void UpdateScoreWhenUserGotUpdateByAmin(UpdateUserScoreViewModel UserScoreReimBurse);
        void UpdateScoreWhenUserGotVote(int UserID, int value);
        void UpdateScoreWhenUserGotThankTo(int userId);

    }
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        IMapper _mapper;

        public UserService(IMapper mapper)
        {
            userRepository = new UserRepository();
            _mapper = mapper;
  
        }
        public List<UserViewModel> GetUsers()
        {
            List<Users> users = userRepository.GetUsers().ToList();
            List<UserViewModel> getUser = _mapper.Map<List<UserViewModel>>(users);
            return getUser;
        }

        public UserViewModel GetUserByEmail(string Email)
        {
            Users user = userRepository.GetUserByEmail(Email).FirstOrDefault();
            if (user == null) return null;
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel GetUserByEmailAndPassword(string Email, string Password)
        {
            Users user = userRepository.GetUserByEmailAndPassword(Email, SHA256Generator.GenerateHash(Password)).FirstOrDefault();
            if (user == null) return null;
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel GetUserByUserId(int userId)
        {
            Users user = userRepository.GetUserByUserID(userId);
            if (user == null) return null;
            return _mapper.Map<UserViewModel>(user);
        }



        public int InsertUser(RegisterViewModel user)
        {
            Users USER = _mapper.Map<Users>(user);
            USER.PasswordHash = SHA256Generator.GenerateHash(user.Password);
            userRepository.InsertUser(USER);
            int userLast = userRepository.GetLatestUserID();
            return userLast;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel user)
        {
            Users userUpdate = _mapper.Map<Users>(user);
            userRepository.UpdateUserDetail(userUpdate);

        }

        public void UpdateUserPassword(EditUserPasswordViewModel userPasswordUpdate)
        {
            Users UpdatePassword = _mapper.Map<Users>(userPasswordUpdate);
            UpdatePassword.PasswordHash = SHA256Generator.GenerateHash(userPasswordUpdate.Password);
            userRepository.UpdateUserPassword(UpdatePassword);
        }

        public void DeleteUser(int UserId)
        {
            userRepository.DeleteUser(UserId);
        }

        public void UpdateScoreWhenAnswerQuestion(int userId)
        {
            userRepository.ScoreUpdateWhenAnswerQuestion(userId);
        }
        public void UpdateScoreWhenUserUpdateAnswer(int userId)
        {
            userRepository.UpdateScoreWhenUserUpdateAnswer(userId);
        }
        public void UserUpdateScoreWhenGotReport(int reportedUserId, int reasonId)
        {
            userRepository.UpdateScoreWhenUserGotReport(reportedUserId, reasonId);
        }

        public void InsertMsgToUser(WarnUserViewModel userMsg)
        {
            Users SendMsg = _mapper.Map<Users>(userMsg);
            userRepository.SentMsgToUser(SendMsg);
        }

        public void UpdateScoreWhenUserGotWarning(int userId)
        {
            userRepository.UpdateScoreWhenUserGotWarnings(userId);
        }

        public void UpdateScoreWhenUserGotUpdateByAmin(UpdateUserScoreViewModel UserScoreReimBurse)
        {
            Users user = _mapper.Map<Users>(UserScoreReimBurse);
            userRepository.UpdateScoreWhenUserGotUpdateByAmin(user);
        }

        public void UpdateScoreWhenUserGotVote(int UserID, int value)
        {
            userRepository.UpdateScoreWhenUserGotVote(UserID, value);
        }

        public void UpdateScoreWhenUserGotThankTo(int userId)
        {
            userRepository.UpdateScoreWhenUserGotThankTo(userId);
        }
    }
}
