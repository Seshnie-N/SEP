using SEP.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    [Table("Employer")]
    public class Employer 
    {
        //[Key]
        //public int EmployerId { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
