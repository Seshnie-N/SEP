using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SEP.Models.Enums;

namespace SEP.Models.DomainModels
{
	public class Post
	{

		[Key]
		[Required]
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
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

		[DisplayName("End date")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

		[DisplayName("Hourly rate")]
		public int hourlyRate { get; set; }

		[DisplayName("Limited to")]
		public bool limitedToSA { get; set; }

		[DisplayName("Minimum requirments")]
		public string minimumRequirment { get; set; }

		[DisplayName("Application instruction")]
		public string applicationInstruction { get; set; }

		[DisplayName("Closing date")]
		public DateTime applicationClosingDate { get; set; }

		[DisplayName("Contact person name")]
		public string conatctPersonName { get; set; }

		[DisplayName("Contact person email")]
		public string conatctPersonEmail { get; set; }

		[DisplayName("Contact person number")]
		public string conatctPersonNumber { get; set; }

		[DisplayName("Reviewer's comment")]
		public string? postreviewComment { get; set; }

		public bool isApproved { get; set; }

		[DisplayName("Outcome")]
		public string approvalStatus { get; set; }

		[DisplayName("Post status")]
		public string postStatus { get; set; }

		public string departmentName { get; set; }

		public int facultyName { get; set; }

		public bool limitedTo1stYear { get; set; }

		public bool limitedTo2ndYear { get; set; }

		public bool limitedTo3rdYear { get; set; }

		public bool limitedToHonours { get; set; }

		public bool limitedToGraduate { get; set; }

		public bool limitedToMasters { get; set; }

		public bool limitedToPhd { get; set; }

		public bool limitedToPostdoc { get; set; }

		public bool limitedToDepartment { get; set; }

		public bool limitedToFaculty { get; set; }

		public string UserID { get; set; }

	}
}
