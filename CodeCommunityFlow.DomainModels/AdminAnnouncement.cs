using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.DomainModels
{
    [Table("AdminAnnouncement")]
    public class AdminAnnouncement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminAnnouncementID { get; set; }
        public string AdminAnnounceTopic { get; set; }
        public string AdminAnnounceContent { get; set; }
        public string ImageContent { get; set; }

        [ForeignKey("AdminID")]
        public virtual AdminUsers AdminUsers { get; set; }
        public int AdminID { get; set; }
        public int CommentCount { get; set; }
        public int VoteCount { get; set; }
        public int ViewCount { get; set; }
        public DateTime AnnouncementDateTime { get; set; }
        public virtual List<CommentFromAnnouncement> CommentFromAnnouncement { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
