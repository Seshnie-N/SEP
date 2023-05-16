using SEP.Areas.Identity.Data;

namespace SEP.Models.DomainModels
{
    public class Employer : ApplicationUser
    {
        public string RegistrationNumber { get; set; }
    }
}
