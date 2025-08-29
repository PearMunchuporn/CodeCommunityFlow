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

    public interface IAnswerService
    {
        void InsertAnswer(newAnswerViewModel ans);
        void UpdateAnswer(EditAnswerViewModel ans);
        void DeleteAnswer(int AnswerID);
        void UpdateAnswerVoteCount(int answerid, int value, int userId);
        void DeleteAnswerByAdmin(int AnswerId, int UserId);
        void ThankToAnswer(int AnswerID);
        List<AnswerViewModel> GetAnswersByQuestionId(int QuestionId);
        AnswerViewModel GetAnswersByAnswerId(int AnswerID);
        List<AnswerViewModel> GetAnswers();
        List<AnswerViewModel> GetAnswerByUser(int UserID);
    }
    public class AnswerService : IAnswerService
    {
        IAnswerRepository answerRepository;
        IUserRepository userRepository;
        IMapper _mapper;
        public AnswerService(IMapper mapper)
        {
            answerRepository = new AnswerRepository();
            userRepository = new UserRepository();
            _mapper = mapper;
        }
        public AnswerViewModel GetAnswersByAnswerId(int AnsId)
        {

            var answer = answerRepository.GetAnswersByAnswerId(AnsId).FirstOrDefault();
            return _mapper.Map<AnswerViewModel>(answer);
        }

        public List<AnswerViewModel> GetAnswersByQuestionId(int QuestionID)
        {
            var answers = answerRepository.GetAnswersByQuestionId(QuestionID);
            return _mapper.Map<List<AnswerViewModel>>(answers);
        }

        public void InsertAnswer(newAnswerViewModel ans)
        {
            var answers = _mapper.Map<Answers>(ans);
            answerRepository.InsertAnswer(answers);
        }

        public void UpdateAnswer(EditAnswerViewModel ans)
        {
            var answers = _mapper.Map<Answers>(ans);
            answerRepository.UpdateAnswer(answers);
        }

        public void UpdateAnswerVoteCount(int answerId,  int value, int userId)
        {
            answerRepository.UpdateAnswerVoteCount(answerId, value, userId);
        }

        public void DeleteAnswer(int AnswerID)
        {
            answerRepository.DeleteAnswer(AnswerID);
        }

        public List<AnswerViewModel> GetAnswers()
        {

            var allAnswers = answerRepository.GetAnswers();
            return _mapper.Map<List<AnswerViewModel>>(allAnswers);
        }

        public void DeleteAnswerByAdmin(int AnswerID, int UserID)
        {
            answerRepository.DeleteAnswer(AnswerID);
            userRepository.UpdateUserScoreWhenAdminDelete(UserID);
            
        }

        public List<AnswerViewModel> GetAnswerByUser(int UserID)
        {

            var allAnswers = answerRepository.GetAnswers();
            return _mapper.Map<List<AnswerViewModel>>(allAnswers);

        }

        public void ThankToAnswer(int AnswerID)
        {
            answerRepository.ThankToAnswer(AnswerID);
        }
    }
}
