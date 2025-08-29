using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.Repository;
using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
namespace CodeCommunityFlow.ServiceLayers
{

    public interface IQuestionService
    {
        void InsertQuestion(newQuestionViewModel questionViewModel);
        void UpdateQuestionDetail(EditQuestionViewModel EditQuestion);
        void UpdateQuestionVoteCount(int QuestionID, int UserID,  int value);
        void UpdateQuestionAnswerCount(int QuestionID, int value);
        void UpdateQuestionViewCount(int QuestionID, int value);
        QuestionViewModel GetQuestionById(int QuestionID, int UserID);
        void DeleteQuestion(int QuestionID);
        void DeleteQuestionByAdmin(int QuestionID, int UserID);
        List<QuestionViewModel> GetQuestions();
    
        List<QuestionViewModel> GetQuestionByUser(int UserID);
    }
    public class QuestionService :IQuestionService
    {

        IQuestionRepository questionRepository;
        IUserRepository userRepository;
        IMapper _mapper;
        public QuestionService(IMapper mapper)
        {
            questionRepository = new QuestionRepository();
            userRepository = new UserRepository();
            _mapper = mapper;
        }
        public List<QuestionViewModel> GetQuestions()
        {
            var questions = questionRepository.GetQuestions().ToList();
            return _mapper.Map<List<QuestionViewModel>>(questions);

        }

        public QuestionViewModel GetQuestionById(int QuestionId, int UserId = 0)
        {
            var question = questionRepository.GetQuestionById(QuestionId);
            if (question == null) return null;

            var questionViewModel = _mapper.Map<QuestionViewModel>(question);

            foreach (var item in questionViewModel.Answers)
            {
                item.CurrentUserVoteType = item.Votes
                    .FirstOrDefault(v => v.UserID == UserId)?.VoteValue ?? 0;
            }

            return questionViewModel;
        }
        public void InsertQuestion(newQuestionViewModel AddQuestionFromView)
        {
            var question = _mapper.Map<Questions>(AddQuestionFromView);
            questionRepository.InsertQuestion(question);
        }
        public void UpdateQuestionDetail(EditQuestionViewModel EditQuestionFromView)
        {
            var question = _mapper.Map<Questions>(EditQuestionFromView);
            questionRepository.UpdateQuestionDetail(question);
        }


        public void UpdateQuestionAnswerCount(int questionId, int value)
        {
            questionRepository.UpdateQuestionAnswerCount(questionId, value);
        }


        public void UpdateQuestionViewCount(int questionId, int value)
        {
            questionRepository.UpdateQuestionViewCount(questionId, value);
        }

        public void UpdateQuestionVoteCount(int questionId,int userId ,int value)
        {
            questionRepository.UpdateQuestionVoteCount(questionId, userId, value);
        }

        public void DeleteQuestion(int questionId)
        {
            questionRepository.DeleteQuestion(questionId);
        }
        public void DeleteQuestionByAdmin(int questionId, int userId)
        {
            questionRepository.DeleteQuestion(questionId);
            userRepository.UpdateUserScoreWhenAdminDelete(userId);
        }

        public List<QuestionViewModel> GetQuestionByUser(int UserID)
        {

            var questions = questionRepository.GetQuestionByUser(UserID);
            return _mapper.Map<List<QuestionViewModel>>(questions);
        }
    }
}
