using SEP.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    public class Employer 
    {
        [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
