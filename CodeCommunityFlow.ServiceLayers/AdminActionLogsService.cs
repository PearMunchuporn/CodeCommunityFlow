using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ViewModels;
using CodeCommunityFlow.Repository;
namespace CodeCommunityFlow.ServiceLayers
{
    public interface IAdminLogsActionService
    {
        List<AdminActionLogsViewModel> GetActions();
        void InsertAction(AdminActionLogsViewModel InsertAction);
    }
    public class AdminActionLogsService : IAdminLogsActionService
    {
        IMapper _mapper;
        IAdminActionLogsReposity ActionLogsReposity;
        public AdminActionLogsService(IMapper mapper)
        {
            _mapper = mapper;
            ActionLogsReposity = new AdminActionLogsReposity();

        }
        public List<AdminActionLogsViewModel> GetActions()
        {
            var actionLogs = ActionLogsReposity.GetAdminActionLogs().ToList();
            return _mapper.Map<List<AdminActionLogsViewModel>>(actionLogs);
        }

        public void InsertAction(AdminActionLogsViewModel InsertAction)
        {
            var actionlogs = _mapper.Map<AdminActionLogs>(InsertAction);
            ActionLogsReposity.InsertActionLogs(actionlogs);
        }
    }
}
