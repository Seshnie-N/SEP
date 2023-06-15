using SEP.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SEP.Models.ViewModels
{
    public class ApplicationViewModel
    {
        public JobApplication Application { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public List<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
    }
}
