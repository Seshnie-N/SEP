using SEP.Areas.Identity.Data;
using SEP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    [Table("Student")]
    public class Student 
    {
        [Key,ForeignKey(nameof(User))]
        public string UserId { get; set; }
        //navigation property
        public virtual ApplicationUser User { get; set; }
        
        public string Address { get; set; }
        public string IdNumber { get; set; }
        //public string Department { get; set; }
        public Gender Gender { get; set; }
        public Race Race { get; set; }
        public bool isSouthAfrican { get; set; }
        public DriversLicense DriversLicense { get; set; }
        public string? CareerObjective { get; set; }

    }
}
