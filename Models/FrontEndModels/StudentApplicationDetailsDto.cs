using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models.FrontEndModels
{
    public class StudentApplicationDetailsDto
    {
        public int postId { get; set; }

        [DisplayName("Job title")]
        public string jobTitle { get; set; }

        [DisplayName("Location")]
        public string jobLocation { get; set; }

        [DisplayName("Description of job")]
        public string jobDescription { get; set; }

        [DisplayName("Key responsibilities")]
        public string resposibilities { get; set; }

        [DisplayName("Fulltime or part-time")]
        public string jobType { get; set; }

        [DisplayName("Part-time: Number of hours required in a week")]
        public string? partTimeHour { get; set; }

        [DisplayName("Start date")]
        public DateTime startDate { get; set; }

        [DisplayName("End date")]
        public DateTime endDate { get; set; }

        [DisplayName("Hourly rate")]
        public int hourlyRate { get; set; }

        [DisplayName("Minimum requirments")]
        public string minimumRequirment { get; set; }

        [DisplayName("Application instruction")]
        public string applicationInstruction { get; set; }

        [DisplayName("Closing date")]
        public DateTime applicationClosingDate { get; set; }

    }
}
