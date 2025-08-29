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
    public interface IReportService
    {
        void InsertReport(ReportViewModel report);
        List<ReportViewModel> GetReports();
        void EditReport(ReportViewModel report);
        ReportViewModel GetReportById(int ReportId);
    }
    public class ReportService:IReportService
    {
        IReportRepository reportRepository;
        IMapper _mapper;
        public ReportService(IMapper mapper)
        {
            reportRepository = new ReportRepository();
            _mapper = mapper;
        }

        public void EditReport(ReportViewModel report)
        {
            var ReportUpdate = _mapper.Map<Report>(report);
            reportRepository.EditReport(ReportUpdate);
        }

        public ReportViewModel GetReportById(int ReportId)
        {
            var report = reportRepository.GetReportByID(ReportId);
            return _mapper.Map<ReportViewModel>(report);
        }

        public List<ReportViewModel> GetReports()
        {
            var Reports = reportRepository.GetReports();
            return _mapper.Map<List<ReportViewModel>>(Reports);
        }

        public void InsertReport(ReportViewModel report)
        {
            var InsertReport = _mapper.Map<Report>(report);
            reportRepository.InsertReport(InsertReport);
        }
    }
}
