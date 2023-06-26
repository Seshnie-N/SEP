using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Qualification
	{
		[Key]
		public Guid QualificationId { get; set; }
		[ForeignKey(nameof(Student))]
		public string StudentId { get; set; }
		public Student Student { get; set; }
        [Required(ErrorMessage = "Enter the name of the institution at which the qualification was obtained.")]
        public string Institution { get; set; }
        [Required(ErrorMessage = "Select a start date for the offered job.")]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Select an end date for the offered job.")]
        [DisplayName("End date")]
        public DateTime EndDate { get; set;}
        [Required(ErrorMessage = "Enter name of qualification.")]
        [Display(Name ="Qualification")]
        public string QualificationName { get; set; }
        public string? Subjects { get; set; }
		public string? Majors { get; set; }
        [DisplayName("Sub-Majors")]
        public string? SubMajors { get; set; }
        public string? Research { get; set; }
    }
}
