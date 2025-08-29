using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class Votes
    {
        public int VoteId { get; set; }
        public int? UserId { get; set; }
        public int? AnswerId { get; set; }
        public int? VoteValue { get; set; }

        public int? AdminID { get; set; }
       
        public virtual Answers Answer { get; set; }
        public virtual Users User { get; set; }
    }
}
