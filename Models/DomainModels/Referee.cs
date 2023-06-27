using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Referee
	{
		[Key]
        public int RefereeId { get; set; }
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }
        public Student Student { get; set; }
        [Required(ErrorMessage = "Please provide a name for the referee.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide a job title for the referee.")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Please provide an institution for the referee.")]
        public string Institution { get; set; }
        [RegularExpression("^(\\+27|0)[6-8][0-9]{8}$", ErrorMessage = "Invalid phone number")]
        [Required(ErrorMessage = "Please provide a cell number for the referee.")]
        public string Cell { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Please provide an email address for the referee.")]
        public string Email { get; set; }
    }
}
