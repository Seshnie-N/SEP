using SEP.Areas.Identity.Data;

namespace SEP.Models.DomainModels
{
    public class Student : ApplicationUser
    {
        public string StudentNumber { get; set; }
    }
}
