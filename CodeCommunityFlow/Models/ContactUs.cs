using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class ContactUs
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkEmail { get; set; }
        public int HowHearUsId { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }

        public virtual HowKnowUs HowHearUs { get; set; }
    }
}
