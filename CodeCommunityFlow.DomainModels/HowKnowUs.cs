using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeCommunityFlow.DomainModels
{
    public class HowKnowUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Please select How do you hear about us")]
        public int HowHearUsID { get; set; }
        public string Know { get; set; }
    }
}