using SEP.Data;
using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
    public class EmployerProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public Employer Employer { get; set; }
    }
}
