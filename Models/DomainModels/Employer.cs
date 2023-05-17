using SEP.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    [Table("Employer")]
    public class Employer 
    {
        [Key]
        public int EmployerId { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
