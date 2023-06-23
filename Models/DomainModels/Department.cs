using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Department
    { 
        [Key]
		public int departmentId { get; set; }

        [ForeignKey(nameof(FacultyId))]
		public int FacultyId { get; set; }
        [DisplayName("Department")]
		public string departmentName { get; set; }

        public Faculty? faculty { get; set; }
    }
}
