using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SEP.CustomValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class WorkExperience
	{
		[Key]
		public Guid WorkExperienceId { get; set; }
		[ForeignKey(nameof(Student))]
		public string StudentId { get; set; }
		[ValidateNever]
		public Student Student { get; set; }
        [DisplayName("Employer's Name")]
        public string EmployerName { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        //[StartAndEndDateValidator("Start Date")]
        public DateTime EndDate { get; set; }
        [DisplayName("Job Title")]
        public string JobTitle { get; set;}
		[DisplayName("Tasks and Responsibilities")]
        public string TasksAndResponsibilities { get; set; }
    }
}
