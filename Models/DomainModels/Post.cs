using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SEP.Data;
using SEP.Models.Enums;

namespace SEP.Models.DomainModels
{
	public class Post
	{
		[Key]
		[Required]
		public Guid PostId { get; set; }

		[ForeignKey(nameof(EmployerId))]
		public string? EmployerId { get; set; }
		public Employer Employer { get; set; }

		//do we even need the department and 

        //need to create foreign key relationship here
        [DisplayName("Department")]
        public string DepartmentName { get; set; }
        //may not need this property anymore
        [DisplayName("Faculty")]
        public int FacultyName { get; set; }
        [Required(ErrorMessage = "Please provide the title of the role being offered.")]
        [DisplayName("Job Title")]
		public string JobTitle { get; set; }
		[Required(ErrorMessage = "Please specify where the job is being offered.")]
		[DisplayName("Location")]
		public string JobLocation { get; set; }
        [Required(ErrorMessage = "Please provide a description of what the job being offered is")]
        [DisplayName("Job Description")]
		public string JobDescription { get; set; }

		[DisplayName("Key responsibilities")]
		public string Responsibilities { get; set; }

		[DisplayName("Fulltime or part-time")]
		public string JobType { get; set; }

		[DisplayName("Part-time: Number of hours required in a week")]
		public string? PartTimeHour { get; set; }

        //need to come back and add validations
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Select a start date for the offered job.")]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Select an end date for the offered job.")]
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18,2)")]
		[DisplayName("Hourly rate")]
		public decimal HourlyRate { get; set; }

		[DisplayName("Limited to")]
		public bool LimitedToSA { get; set; }

		[DisplayName("Minimum requirments")]
		public string MinimumRequirement { get; set; }

		[DisplayName("Application instruction")]
		public string ApplicationInstruction { get; set; }

		[DisplayName("Closing date")]
		public DateTime ApplicationClosingDate { get; set; }

		[DisplayName("Contact person name")]
		public string ContactPersonName { get; set; }

		[DisplayName("Contact person email")]
		[EmailAddress]
		public string ContactPersonEmail { get; set; }

		[DisplayName("Contact person number")]
        [RegularExpression("^(\\+27|0)[6-8][0-9]{8}$", ErrorMessage = "Invalid phone number")]
        public string ContactPersonNumber { get; set; }

		[DisplayName("Reviewer's comment")]
		public string? PostReviewComment { get; set; }

		public bool IsApproved { get; set; }

		[DisplayName("Outcome")]
		public string ApprovalStatus { get; set; }

		[DisplayName("Post status")]
		public string PostStatus { get; set; }

		public bool LimitedTo1stYear { get; set; }

		public bool LimitedTo2ndYear { get; set; }

		public bool LimitedTo3rdYear { get; set; }

		public bool LimitedToHonours { get; set; }

		public bool LimitedToGraduate { get; set; }

		public bool LimitedToMasters { get; set; }

		public bool LimitedToPhd { get; set; }

		public bool LimitedToPostdoc { get; set; }

		public bool LimitedToDepartment { get; set; }

		public bool LimitedToFaculty { get; set; }
		
	}
}
