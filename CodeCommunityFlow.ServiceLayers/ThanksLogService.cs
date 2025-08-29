using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.Repository;
using AutoMapper;

namespace CodeCommunityFlow.ServiceLayers
{
    public interface IThankLogsService
    {
        void InsertThankLogs(ThanksLogsViewModel thanksLogs);
    }
    public class ThanksLogService: IThankLogsService
    {
        IThanksLogsRepository thanksLogsRepository;
        IMapper _mapper;
        public ThanksLogService(IMapper mapper)
        {
            thanksLogsRepository = new ThanksLogsRepository();
            _mapper = mapper;
        }

        public void InsertThankLogs(ThanksLogsViewModel thanksLogs)
        {
            var ThankLogsInsert = _mapper.Map<ThanksLogs>(thanksLogs);
            thanksLogsRepository.InsertThankLogs(ThankLogsInsert);
        }
    }
}
