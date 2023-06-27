using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Qualification
	{
		[Key]
		public int QualificationId { get; set; }
		[ForeignKey(nameof(Student))]
		public string StudentId { get; set; }
		public Student Student { get; set; }
        [Required(ErrorMessage = "Please provide an institution.")]
        public string Institution { get; set; }
		[DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Please provide a valid start date.")]
        public DateTime StartDate { get; set; }
		[DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Please provide a valid end date.")]
        public DateTime EndDate { get; set;}
		[Display(Name ="Qualification")]
        [Required(ErrorMessage = "Please provide the name of your qualification.")]
        public string QualificationName { get; set; }
        [Required(ErrorMessage = "Please provide the subjects related to the qualification.")]
        public string Subjects { get; set; }
        [Required(ErrorMessage = "Please provide the majors related to the qualification.")]
        public string Majors { get; set; }
        [Display(Name = "Sub-Majors")]
        public string? SubMajors { get; set; }
        public string? Research { get; set; }
    }
}
