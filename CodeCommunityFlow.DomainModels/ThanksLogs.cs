using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.DomainModels
{
    public class ThanksLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThankedID { get; set; }
        public int ThankedUserID { get; set; }
        public int UserIDGaveThank { get; set; }
        public int ThankedAnswerID { get; set; }

    }
}
