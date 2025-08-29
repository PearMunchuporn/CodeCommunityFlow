using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace CodeCommunityFlow.Repository
{ 

    public interface IDeleteLogsRepository
    {
        List<DeleteLogs> GetDeleteLogs();
        void InsertDeleteLogs(DeleteLogs deleteLogs);

    }
     public class DeleteLogsRepository : IDeleteLogsRepository
    {
        CodeCommunityFlowDbContext db;

        public DeleteLogsRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public List<DeleteLogs> GetDeleteLogs()
        {

            //return db.DeleteLogs
            //         .Include(d => d.Question)
            //         .Include(d => d.Answer)
            //         .Include(d => d.User)
            //         .ToList();
          List<DeleteLogs> deleteLogs = db.DeleteLogs.ToList();
            return deleteLogs;
        }

        public void InsertDeleteLogs(DeleteLogs deleteLogs)
        {
            
            db.DeleteLogs.Add(deleteLogs);
            db.SaveChanges();
        }
    }
}
