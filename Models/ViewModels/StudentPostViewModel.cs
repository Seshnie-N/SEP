using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace SEP.Models.ViewModels
{
    public class StudentPostViewModel
    {
        public Guid PostId { get; set; }

        [DisplayName("Job Title")]
        public string JobTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentName { get; set; }

        [DisplayName("FT/PT")]
        public string JobType { get; set; }

        [DisplayName("Start")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DisplayName("Hours per week")]
        public string? PartTimeHour { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Hourly rate")]
        public decimal HourlyRate { get; set; }
    }
}
