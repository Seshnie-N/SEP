using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
    public class StudentProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public Student Student { get; set; }
    }
}
