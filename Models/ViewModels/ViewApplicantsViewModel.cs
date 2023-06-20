using SEP.Models.DomainModels;
using System.ComponentModel;

namespace SEP.Models.ViewModels
{
    public class ViewApplicantsViewModel
    {
        public int PostId { get; set; }
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }
        [DisplayName("Job Description")]
        public string JobDescription { get; set; }
        public IEnumerable<JobApplication> Applications { get; set; }
    }
}
