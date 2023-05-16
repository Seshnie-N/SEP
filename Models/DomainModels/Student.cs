using SEP.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    public class Student 
    {
        [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string StudentNumber { get; set; }
    }
}
