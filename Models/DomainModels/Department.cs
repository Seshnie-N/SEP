using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Department
	{
		// public Department(int depId, int facId, string depName)
        // {
         //   departmentId = depId;

        //    FacultyId = facId;
        //   departmentName = depName;
        // }
        

        [Key]
		public int departmentId { get; set; }

		public string departmentName { get; set; }

        public Faculty? faculty { get; set; }

        [ForeignKey(nameof(FacultyId))]
		public int FacultyId { get; set; }

        //public ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();	
    }
}
