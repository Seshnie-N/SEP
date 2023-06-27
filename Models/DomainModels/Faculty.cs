using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SEP.Models.DomainModels
{
	public class Faculty
	{
		[Key]
		public int FacultyId { get; set; }
		[DisplayName("Faculty")]
		public string FacultyName { get; set; }
	}
}
