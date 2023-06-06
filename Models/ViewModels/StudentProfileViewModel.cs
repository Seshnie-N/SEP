using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
    public class StudentProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public Student Student { get; set; }
        public IEnumerable<Faculty> faculty { get; set; }
        public IEnumerable<Department> department { get; set; }
        public IEnumerable<Qualification>? Qualifications { get; set; } = new List<Qualification>();
		public IEnumerable<WorkExperience>? WorkExperience { get; set; } = new List<WorkExperience>();
		public IEnumerable<Referee>? Referee { get; set; } = new List<Referee>();
	}
}
