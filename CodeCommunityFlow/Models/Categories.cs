using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Questions = new HashSet<Questions>();
        }

        public int CategoryId { get; set; }
        public string CatagoryName { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
