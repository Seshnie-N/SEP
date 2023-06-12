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
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Institution { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
    }
}
