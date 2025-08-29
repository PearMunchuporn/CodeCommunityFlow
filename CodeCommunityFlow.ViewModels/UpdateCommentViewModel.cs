using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
   public class UpdateCommentViewModel
    {
        public int CommentID { get; set; }
        public string CommentContent { get; set; }
        public int VoteCount { get; set; }
        public string Image { get; set; }
        public string ImageOld { get; set; }
        public int UserID { get; set; }
        public DateTime CommentDateTime { get; set; }
        public virtual UserViewModel Users { get; set; }
        public virtual List<VoteViewModel> Votes { get; set; }
        public int AdminAnnouncementID { get; set; }
        public int CurrentUserVoteType { get; set; }

        public string Description { get; set; }
    }
}
