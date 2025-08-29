using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class HowKnowUs
    {
        public HowKnowUs()
        {
            ContactUs = new HashSet<ContactUs>();
        }

        public int HowHearUsId { get; set; }
        public string Know { get; set; }

        public virtual ICollection<ContactUs> ContactUs { get; set; }
    }
}
