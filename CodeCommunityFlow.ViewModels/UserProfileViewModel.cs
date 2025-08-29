using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class UserProfileViewModel
    {
        public UserViewModel User { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
        public List<DeleteLogsViewModel> DeleteLogs { get; set; }
        public List<ScoreHistoryViewModel> ScoreHistoryUser { get; set; }
        public string ImageUser { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int WarningCount { get; set; }
        public string WarningMessage { get; set; }
        public string OldWarningMsg { get; set; }
        public int Score { get; set; }


    }
}
