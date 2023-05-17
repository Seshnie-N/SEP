using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.ViewModels
{
	public class StudentProfile
	{
		public int StudentId { get; set; }
		public string UserId { get; set; }
		public string StudentNumber { get; set; }
	}
}
