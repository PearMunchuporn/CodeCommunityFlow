using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.DomainModels
{
    [Table("CommentFromAnnouncement")]
    public class CommentFromAnnouncement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        public string CommentContent { get; set; }
        public int VoteCount { get; set; }
        public string Image { get; set; }
        public DateTime CommentDateTime { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual Users Users { get; set; }
        public virtual List<Votes> Votes { get; set; }
        public int AdminAnnouncementID { get; set; }
        [ForeignKey("AdminAnnouncementID")]
        public virtual AdminAnnouncement Announcement{ get; set; }
        public string Description { get; set; }
        [InverseProperty("CommentFromAnnoucement")]
        public virtual List<ReportLogs> ReportLogs { get; set; }

        [InverseProperty("CommentFromAnnoucement")]
        public virtual List<DeleteLogs> DeleteLogs { get; set; }
    }
}
