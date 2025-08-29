using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.Repository
{
    public interface IReportRepository
    {
        void InsertReport(Report report);
        List<Report> GetReports();
        void EditReport(Report report);
        Report GetReportByID(int Report_id);

    }
    public class ReportRepository : IReportRepository
    {
        CodeCommunityFlowDbContext db;
        public ReportRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public void EditReport(Report reportUpdate)
        {
            Report report = db.Report.Where(r => r.ReportID == reportUpdate.ReportID).FirstOrDefault();
            if (report != null)
            {
                report.ReportReason = reportUpdate.ReportReason;
                report.ScoreMinus = reportUpdate.ScoreMinus;
                db.SaveChanges();
            }
           
        }

        public Report GetReportByID(int Report_id)
        {
            Report reports = db.Report.Where(r => r.ReportID == Report_id).FirstOrDefault();
            return reports;
        }

        public List<Report> GetReports()
        {
            List<Report> reports = db.Report.ToList();
            return reports;
        }

        public void InsertReport(Report report)
        {
            db.Report.Add(report);
            db.SaveChanges();
        }
    }
}
