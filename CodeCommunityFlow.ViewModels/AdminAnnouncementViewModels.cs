using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.ViewModels
{
    public class AdminAnnouncementViewModels
    {
        public int AdminAnnouncementID { get; set; }
        [Required(ErrorMessage = "Please add topic of annoucement")]
        public string AdminAnnounceTopic { get; set; }
        public string AdminAnnounceContent { get; set; }
        public string ImageContent { get; set; }
        public DateTime AnnouncementDateTime { get; set; }
        public int CommentCount { get; set; }
        public int VoteCount { get; set; }
        public virtual AdminUserViewModels AdminUsers { get; set; }
        public virtual UserViewModel Users { get; set; }
        public int AdminID { get; set; }
        public int UserID { get; set; }
        public int ViewCount { get; set; }
        public string Category { get; set; }
        public virtual List<CommentFromAnnouncementViewModel> CommentFromAnnouncement { get; set; }
        public virtual List<ReportLogsViewModel> ReportReason { get; set; }
        public string ImgOld { get; set; } 
        public string Description { get; set; }
        public int CurrentUserVoteType { get; set; }
    }
}
