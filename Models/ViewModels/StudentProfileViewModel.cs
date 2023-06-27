using SEP.Data;
using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
    public class StudentProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public Student Student { get; set; }
        public IEnumerable<Faculty> Faculty { get; set; }
        public IEnumerable<Department> Department { get; set; }
        public IEnumerable<Qualification>? Qualifications { get; set; } = new List<Qualification>();
		public IEnumerable<WorkExperience>? WorkExperience { get; set; } = new List<WorkExperience>();
		public IEnumerable<Referee>? Referee { get; set; } = new List<Referee>();
	}

    public class UpdateStudentViewModel
    {
        public ApplicationUser User { get; set;}
        public Student Student { get; set;}
    }
}
