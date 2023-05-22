using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models.DomainModels
{
	public class Department
	{
		[Key]
		public int departmentId { get; set; }

		public string departmentName { get; set; }

		public ICollection<Faculty> faculties { get; set; } = new List<Faculty>();	
	}
}
