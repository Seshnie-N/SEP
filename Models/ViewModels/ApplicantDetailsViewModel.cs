using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
    public class ApplicantDetailsViewModel
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public JobApplication Application { get; set; }
        public IEnumerable<ApplicationDocument> Documents { get; set; }
    }
}
