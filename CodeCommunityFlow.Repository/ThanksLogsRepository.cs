using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;

namespace CodeCommunityFlow.Repository
{

   public interface IThanksLogsRepository
    {
        void InsertThankLogs(ThanksLogs thanksLogs);
    }
   public class ThanksLogsRepository: IThanksLogsRepository
    {
        CodeCommunityFlowDbContext db;
        public ThanksLogsRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public void InsertThankLogs(ThanksLogs thanksLogs)
        {
            db.ThanksLogs.Add(thanksLogs);
            db.SaveChanges();
        }
    }
    
}
