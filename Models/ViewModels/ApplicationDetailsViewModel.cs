using SEP.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.ViewModels
{
    public class ApplicationDetailsViewModel
    {
        public JobApplication JobApplication { get; set; }
        public IEnumerable<Faculty> faculty { get; set; }

        public IEnumerable<Department> department { get; set; }

        public IEnumerable<PartTimeHours> partTimeHours { get; set; }
        public List<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();

    }
}
