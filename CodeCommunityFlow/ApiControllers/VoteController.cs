using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.ViewModels;
using System.Web.Http;

namespace CodeCommunityFlow.ApiControllers
{
    [RoutePrefix("api/vote")]
    public class VoteController : ApiController
    {
        IAnswerService answerService;
        IQuestionService questionService;
        IUserService userService;
        IScoreHistoryLogsService scoreHistoryLogsService;
        ICommentFromAnnouncementService commentFromAnnouncementService;
        IAdminAnnounceService adminAnnounceService;
        public VoteController(IAnswerService answerService, IQuestionService questionService,
            IUserService userService, IScoreHistoryLogsService scoreHistoryLogsService, ICommentFromAnnouncementService commentFromAnnouncementService, IAdminAnnounceService adminAnnounceService)
        {
            this.answerService = answerService;
            this.questionService = questionService;
            this.userService = userService;
            this.scoreHistoryLogsService = scoreHistoryLogsService;
            this.commentFromAnnouncementService = commentFromAnnouncementService;
            this.adminAnnounceService = adminAnnounceService;
        }
        [HttpPost]
        [Route("UpdateVoteAnswer")]
        public IHttpActionResult UpdateVoteAnswer(int answerid, int value, int userId)
        {
            this.answerService.UpdateAnswerVoteCount(answerid, value, userId);
            this.userService.UpdateScoreWhenUserGotVote(userId, value);

            var GotUpVote = 0;
            var GotDownVote = 0;
            if (value>0)
            {
                GotUpVote = 5;
            }else
            {
                GotDownVote = -5;
            }

            var GotVoteAnswer = new ScoreHistoryViewModel
            {
                UserID =userId, //User who got voted
                GotVoteDown = GotDownVote,
                GotVoteUp = GotUpVote,
            };

            this.scoreHistoryLogsService.InsertScoreHistory(GotVoteAnswer);
            return Ok();
        }

        [Route("UpdateVoteQuestion")]
        public void UpdateVoteQuestion(int QuestionID, int UserId, int value) {
        
            this.questionService.UpdateQuestionVoteCount(QuestionID, UserId, value);
            this.userService.UpdateScoreWhenUserGotVote(UserId, value);

            var GotUpVote = 0;
            var GotDownVote = 0;
            if (value > 0)
            {
                GotUpVote = 5;
            }
            else
            {
                GotDownVote = -5;
            }

            var GotVoteQuestion = new ScoreHistoryViewModel
            {
                UserID = UserId, //User who got voted
                GotVoteDown = GotDownVote,
                GotVoteUp = GotUpVote,
            };

            this.scoreHistoryLogsService.InsertScoreHistory(GotVoteQuestion);
        }

        [HttpPost]
        [Route("UpdateVoteComment")]
        public IHttpActionResult UpdateVoteComment(int commentid, int value, int userId)
        {
            this.commentFromAnnouncementService.UpdateCommentVoteCount(commentid, value, userId);
            this.userService.UpdateScoreWhenUserGotVote(userId, value);

            var GotUpVote = 0;
            var GotDownVote = 0;
            if (value > 0)
            {
                GotUpVote = 5;
            }
            else
            {
                GotDownVote = -5;
            }

            var GotVoteComment = new ScoreHistoryViewModel
            {
                UserID = userId, //User who got voted
                GotVoteDown = GotDownVote,
                GotVoteUp = GotUpVote,
            };

            this.scoreHistoryLogsService.InsertScoreHistory(GotVoteComment);
            return Ok();
        }

        [HttpPost]
        [Route("UpdateVoteAnnounce")]
        public void UpdateVoteAnnounce(int announceId, int value, int adminId)
        {
            this.adminAnnounceService.UpdateVoteAnnnouncement(announceId, value, adminId);
  
         
           
        }


    }
}
