using Microsoft.AspNetCore.Mvc.Rendering;
using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
    public class ApplicantDetailsViewModel
    {
        public Guid PostId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public JobApplication Application { get; set; }
        public IEnumerable<ApplicationDocument> Documents { get; set; }
        public string Faculty { get; set; }
        public IEnumerable<Qualification> Qualifications { get; set; } = new List<Qualification>();
        public IEnumerable<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
        public IEnumerable<Referee> Referee { get; set; } = new List<Referee>();
        public string Status { get; set; }  
        public List<SelectListItem> statusList { get; set; }
    }
}
