using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SEP.Models.DomainModels
{
	public class Faculty
	{
		[Key]
		public int facultyId { get; set; }
		public string facultyName { get; set; }

		Department department { get; set; }
	}
}
