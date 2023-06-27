using System.ComponentModel;
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
		[DisplayName("Employer Name")]
        [Required(ErrorMessage = "Please provide a name for the employer.")]
        public string EmployerName { get; set; }
		[DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
		[DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Please provide a job title for the referee.")]
        [DisplayName("Job Title")]
        public string JobTitle { get; set;}
		[DisplayName("Tasks and Responsibility")]
        public string? TasksAndResponsibilities { get; set; }
    }
}
