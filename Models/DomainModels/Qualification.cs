using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
		[ValidateNever]
		public Student Student { get; set; }
        [Required(ErrorMessage = "Enter the name of the institution at which the qualification was obtained.")]
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
        public string? Subjects { get; set; }
		public string? Majors { get; set; }
        [DisplayName("Sub-Majors")]
        public string? SubMajors { get; set; }
        public string? Research { get; set; }
    }
}
