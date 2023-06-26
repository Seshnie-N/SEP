using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Referee
	{
		[Key]
        public Guid RefereeId { get; set; }
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }
		[ValidateNever]
		public Student Student { get; set; }
        [Required(ErrorMessage ="Enter referee's name.")]
        public string Name { get; set; }
        [DisplayName("Job Title")]
        [Required(ErrorMessage = "Enter referee's job title.")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage ="Enter the name of the institution this referee belongs to.")]
        public string Institution { get; set; }
        [DisplayName("Phone Number")]
        [RegularExpression("^(\\+27|0)[6-8][0-9]{8}$", ErrorMessage = "Invalid phone number")]
        public string? Cell { get; set; }
        [Required(ErrorMessage = "Enter an email address for this referee.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
