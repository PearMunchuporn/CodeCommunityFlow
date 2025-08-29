using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.DomainModels
{
    [Table("ContactUS")]
    public class Contact
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkEmail { get; set; }
        public int HowHearUsID { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }

        [ForeignKey("HowHearUsID")]
        public virtual HowKnowUs HowKnowUs { get; set; }
    }
}
