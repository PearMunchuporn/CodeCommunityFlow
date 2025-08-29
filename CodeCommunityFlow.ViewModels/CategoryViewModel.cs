using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommunityFlow.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Please select category")]
        public int CategoryID { get; set; }
        [Required]
        [Display(Name ="Category Name")]
        public string CatagoryName { get; set; }
    }
}
