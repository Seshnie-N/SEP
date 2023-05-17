using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models.DomainModels
{
	public class Department
	{
		[Key]
		public int departmentId { get; set; }

		public string departmentName { get; set; }

		public Faculty faculty { get; set; }
	}
}
