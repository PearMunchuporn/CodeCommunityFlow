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
  
   public interface IScoreHistoryLogsService
    {
        void InsertScoreHistory(ScoreHistoryViewModel scoreHistory);
        List<ScoreHistoryViewModel> GetScoreHistoryLogs();
    }
   public class ScoreHistoryServiceLogs : IScoreHistoryLogsService
    {
        IScoreRepositoryLogs scoreRepositoryLogs;
        IMapper _mapper;
        public ScoreHistoryServiceLogs(IMapper mapper)
        {
            scoreRepositoryLogs = new ScoreHistoryLogsRepository();
            _mapper = mapper;
        }

        public List<ScoreHistoryViewModel> GetScoreHistoryLogs()
        {

            var ScoreLogs = scoreRepositoryLogs.GetScoreHistoryLogs();
            return _mapper.Map<List<ScoreHistoryViewModel>>(ScoreLogs);    
            
        }


        public void InsertScoreHistory(ScoreHistoryViewModel scoreHistoryView)
        {
            var score = _mapper.Map<ScoreHistoryLogs>(scoreHistoryView);
            scoreRepositoryLogs.InsertScoreHistory(score);
        }


    }
}

