using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.Repository
{

    public interface IUserRepository
    {
        void InsertUser(Users users);
        void UpdateUserDetail(Users users);
        void UpdateUserPassword(Users users);
        void DeleteUser(int UserId);
        void SentMsgToUser(Users user);
        void UpdateScoreWhenUserGotWarnings(int UserId);
        List<Users> GetUsers();
        List<Users> GetUserByEmailAndPassword(string Email, string Password);
        List<Users> GetUserByEmail(string Email);
     
        Users GetUserByUserID(int userId);
        int GetLatestUserID();
        void ScoreUpdateWhenAnswerQuestion(int UserId);
        void UpdateScoreWhenUserUpdateAnswer(int UserId);
        void UpdateScoreWhenUserGotReport(int reportedUserId, int reasonid);
        void UpdateUserScoreWhenAdminDelete(int UserId);
        void UpdateScoreWhenUserGotUpdateByAmin(Users UserScoreUpdate);
        void UpdateScoreWhenUserGotVote(int UserId, int value);
        void UpdateScoreWhenUserGotThankTo(int userId);
      

    }
    public class UserRepository :IUserRepository
    {
        CodeCommunityFlowDbContext db;
        public UserRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public List<Users> GetUsers()
        {
            List<Users> users = db.Users.Where(u => u.IsAdmin == false).ToList();
            return users;
        }
        public List<Users> GetUserByEmail(string Email)
        {
            List<Users> usersEmail = db.Users.Where(u => u.Email == Email).ToList();
            return usersEmail;
        }

        public List<Users> GetUserByEmailAndPassword(string Email, string PasswordHash)
        {


            List<Users> usersEmailPassword = db.Users.Where(u => u.Email == Email && u.PasswordHash == PasswordHash).ToList();
            return usersEmailPassword;
        }

        public Users GetUserByUserID(int userId)
        {
            Users user = db.Users.Where(u => u.UserID == userId).FirstOrDefault();
            return user;
        }
        public int GetLatestUserID()
        {
            int LastUser = db.Users.Select(u => u.UserID).Max();
            return LastUser;
        }

        public void InsertUser(Users user)
        {
            user.IsAdmin = false;
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUserDetail(Users userUpdate)
        {
            Users user = db.Users.Where(u => u.UserID == userUpdate.UserID).FirstOrDefault();
            if (user != null)
            {
                user.UserName = userUpdate.UserName;
                user.Email = userUpdate.Email;
                user.ImageUser = userUpdate.ImageUser;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(Users userUpdatePassword)
        {
            Users user = db.Users.Where(u => u.UserID == userUpdatePassword.UserID).FirstOrDefault();


            if (user != null)
            {
                user.PasswordHash = userUpdatePassword.PasswordHash;
                db.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null)
                return;

            // Clear report references
            var reportedUser = db.ReportLogs.Where(r => r.ReportedUserID == userId).ToList();
            foreach (var log in reportedUser)
                log.ReportedUserID = null;

            var userReporter = db.ReportLogs.Where(r => r.ReportedByUserID == userId).ToList();
            foreach (var log in userReporter)
                log.ReportedByUserID = null;

            var DeleteQuestion = db.ReportLogs.Where(r => r.Question != null && r.Question.UserID == userId).ToList();
            foreach (var log in DeleteQuestion)
                log.QuestionID = null;

            var DeleteAnswer = db.ReportLogs.Where(r => r.Answer != null && r.Answer.UserID == userId).ToList();
            foreach (var log in DeleteAnswer)
                log.AnswerID = null;

           



            // Remove user's answers
            var userAnswers = db.Answers.Where(a => a.UserID == userId).ToList();
            db.Answers.RemoveRange(userAnswers);

            var userComment = db.CommentFromAnnouncement.Where(c => c.UserID == userId).ToList();
            db.CommentFromAnnouncement.RemoveRange(userComment);

            db.SaveChanges(); // Save before deleting user
          

            db.Users.Remove(user);
            db.SaveChanges();
        }


        public void ScoreUpdateWhenAnswerQuestion(int UserId)
        {
            Users user = db.Users.Where(u => u.UserID == UserId).FirstOrDefault();

            int value = 10;
            if (user != null)
            {          
                user.Score += value;
                db.SaveChanges();
            }
        }
        public void UpdateScoreWhenUserUpdateAnswer(int UserId)
        {
            Users user = db.Users.Where(u => u.UserID == UserId).FirstOrDefault();
            int value = 5;
            if (user != null)
            {
                
                user.Score +=value;
                db.SaveChanges();
            }
        }
        public void UpdateScoreWhenUserGotReport(int reportedUserId, int reasonId)
        {
            Users user = db.Users.Where(u => u.UserID == reportedUserId).FirstOrDefault();
            var report = db.Report.Where(r => r.ReportID == reasonId).FirstOrDefault(); 

            if (user != null && report != null && report.ScoreMinus != null)
            {
                user.Score += report.ScoreMinus;
                db.SaveChanges();
            }
         
        }

        public void UpdateUserScoreWhenAdminDelete(int UserId)
        {
            Users user = db.Users.Where(u => u.UserID == UserId).FirstOrDefault();
            int value = -50;
            if (user != null)
            {
                user.Score += value;
                user.DeletebyAdminCount += 1;

                db.SaveChanges();
            }

        }

        public void SentMsgToUser(Users user)
        {
            Users FindUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
           
            if(FindUser!=null)
            {
                FindUser.WarningCount += 1;
                FindUser.WarningMessage = user.WarningMessage;
                db.SaveChanges();
            }
        }

        public void UpdateScoreWhenUserGotWarnings(int uid)
        {
            Users user = db.Users.Where(u => u.UserID == uid).FirstOrDefault();
            int value = 30;
            if (user != null)
            {
                user.Score -= value;
                db.SaveChanges();
            }
            
        }

        public void UpdateScoreWhenUserGotUpdateByAmin(Users UserScoreUpdate)
        {
            Users user = db.Users.Where(u => u.UserID == UserScoreUpdate.UserID).FirstOrDefault();
            if (user != null)
            {
                user.Score = UserScoreUpdate.Score;
                db.SaveChanges();

            }

        }

        public void UpdateScoreWhenUserGotVote(int UserId, int value)
        {
            Users user = db.Users.Where(u => u.UserID == UserId).FirstOrDefault();

            if(user!=null)
            {
                if(value >0)
                {
                    user.Score += 5;
                }
                else
                {
                    user.Score -= 5;
                }
                db.SaveChanges();
            }
     
        }

        public void UpdateScoreWhenUserGotThankTo(int userId)
        {
            Users user = db.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {  
                user.Score +=30;
                db.SaveChanges();
            }
        }

    }
}
