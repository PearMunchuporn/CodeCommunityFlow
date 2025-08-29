using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;

namespace CodeCommunityFlow.Repository
{
    public interface IScoreRepositoryLogs
    {
        void InsertScoreHistory(ScoreHistoryLogs scoreHistoryLogs);
        List<ScoreHistoryLogs> GetScoreHistoryLogs();
    }
    public class ScoreHistoryLogsRepository : IScoreRepositoryLogs
    {
        CodeCommunityFlowDbContext db;

        public ScoreHistoryLogsRepository()
        {
            db =new CodeCommunityFlowDbContext();
        }
        public List<ScoreHistoryLogs> GetScoreHistoryLogs()
        {
            List<ScoreHistoryLogs> scoreHistoryLogs = db.ScoreHistoryLogs.ToList();
            return scoreHistoryLogs;
        }

        public void InsertScoreHistory(ScoreHistoryLogs scoreHistoryLogs)
        {
            Users user = db.Users.Where(u => u.UserID == scoreHistoryLogs.UserID).FirstOrDefault();
            var report = db.Report.Where(r => r.ReportID == scoreHistoryLogs.ReportReasonID).FirstOrDefault();
          
            if (user.Score !=0)
            {
                scoreHistoryLogs.OldScore = user.Score; //If user has old score.
            }
            if(user.Score!=0 && report !=null) //If user got report
            {
                scoreHistoryLogs.GotReport = report.ScoreMinus;
            }

            db.ScoreHistoryLogs.Add(scoreHistoryLogs);
            db.SaveChanges();
        }
    }
}
