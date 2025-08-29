using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCommunityFlow.DomainModels
{
   public class AdminUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public bool isAdmin { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


    }
}
