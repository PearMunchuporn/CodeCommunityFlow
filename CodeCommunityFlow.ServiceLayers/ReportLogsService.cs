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
    public interface IReportLogsService
    {
        void InsertReportLogs(ReportLogsViewModel reportLogs);
        List<ReportLogsDTO> GetReportLogs();
     
    }
    public class ReportLogsService : IReportLogsService
    {
        IReportLogsRepository reportLogsRepository;
        IMapper _mapper;
        public ReportLogsService(IMapper mapper)
        {
            reportLogsRepository = new ReportLogsRepository();
            _mapper = mapper;
        }
        public List<ReportLogsDTO> GetReportLogs()
        {
            var ReportLogs = reportLogsRepository.GetReportLogs();
            return _mapper.Map<List<ReportLogsDTO>>(ReportLogs);
        }

        public void InsertReportLogs(ReportLogsViewModel reportLogs)
        {
            var reportLogsInsert = _mapper.Map<ReportLogs>(reportLogs);
            reportLogsRepository.InsertReportLogs(reportLogsInsert);
        }
    }
}
