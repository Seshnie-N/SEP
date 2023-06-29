using SEP.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.ViewModels
{
    public class ApplicationDetailsViewModel
    {
        public JobApplication JobApplication { get; set; }
        public IEnumerable<Faculty> Faculty { get; set; }

        public IEnumerable<Department> Department { get; set; }

        public IEnumerable<PartTimeHours> PartTimeHours { get; set; }
        public List<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();

    }
}
