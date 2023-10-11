using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace AdminPanel.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
