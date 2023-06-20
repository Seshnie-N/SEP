using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SEP.Models.ViewModels
{
    public class StudentPostListVM
    {
        public int postId { get; set; }

        [DisplayName("Job Title")]
        public string jobTitle { get; set; }

        [DisplayName("Department")]
        public string departmentName { get; set; }

        [DisplayName("FT/PT")]
        public string jobType { get; set; }

        [DisplayName("Start")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [DisplayName("End")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        [DisplayName("Hours per week")]
        public string? partTimeHour { get; set; }

        [DisplayName("Rate")]
        public int hourlyRate { get; set; }
    }
}
