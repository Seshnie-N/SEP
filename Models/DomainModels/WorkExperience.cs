using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class WorkExperience
	{
		[Key]
		public int WorkExperienceId { get; set; }
		[ForeignKey(nameof(Student))]
		public string StudentId { get; set; }
		public Student Student { get; set; }
        public string EmployerName { get; set; }
		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }
		[DataType(DataType.Date)]
		public DateTime EndDate { get; set; }
		public string JobTitle { get; set;}
        public string TasksAndResponsibilities { get; set; }
    }
}
