using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.Repository;
using CodeCommunityFlow.ViewModels;

namespace CodeCommunityFlow.ServiceLayers
{

    public interface IDeleteLogsService
    {
        void InsertDeleteLogs(DeleteLogsViewModel deleteLogs);
        List<DeleteLogsViewModel> GetDeleteLogs();
    }
    public class DeleteLogsService : IDeleteLogsService
    {
        IDeleteLogsRepository deleteLogsRepository;
        IMapper _mapper;

        public DeleteLogsService(IMapper mapper)
        {
            deleteLogsRepository = new DeleteLogsRepository();
            _mapper = mapper;
        }

        public List<DeleteLogsViewModel> GetDeleteLogs()
        {
            var DeleteLogs = deleteLogsRepository.GetDeleteLogs();
            return _mapper.Map<List<DeleteLogsViewModel>>(DeleteLogs);
        }


        public void InsertDeleteLogs(DeleteLogsViewModel deleteInsert)
        {
            var InsertLogsDelete = _mapper.Map<DeleteLogs>(deleteInsert);
            deleteLogsRepository.InsertDeleteLogs(InsertLogsDelete);
        }

    }
}
