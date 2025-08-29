using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.DomainModels
{
    public class AdminActionLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionID { get; set; }
        public string ActionName { get; set; }
      
        public int ActionByAdminID { get; set; }
 
        public int? UserID { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string UserName { get; set; }
        [ForeignKey("ActionByAdminID")]
        public virtual AdminUsers Admin {get;set;}
        [ForeignKey("UserID")]
        public virtual Users User { get; set; }
    }
}
