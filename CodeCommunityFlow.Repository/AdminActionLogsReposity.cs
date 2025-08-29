using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
namespace CodeCommunityFlow.Repository
{

    public interface IAdminActionLogsReposity
    {
        List<AdminActionLogs> GetAdminActionLogs();
        void InsertActionLogs(AdminActionLogs InsertActionLogs);
    }
    public class AdminActionLogsReposity: IAdminActionLogsReposity
    {
        CodeCommunityFlowDbContext db;
        public AdminActionLogsReposity()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public List<AdminActionLogs> GetAdminActionLogs()
        {
            var ActionLogs = db.AdminActionLogs.ToList();
            return ActionLogs;
        }

        public void InsertActionLogs(AdminActionLogs InsertActionLogs)
        {
            db.AdminActionLogs.Add(InsertActionLogs);
            db.SaveChanges();
        }
    }
}
