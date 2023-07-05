using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
		[ValidateNever]
		public Employer Employer { get; set; }

		//need to create foreign key relationship here
		[Required(ErrorMessage = "Please select a department.")]
		[DisplayName("Department")]
        public int DepartmentName { get; set; }
		//may not need this property anymore
		[Required(ErrorMessage = "Please select a department.")]
		[DisplayName("Faculty")]
        public int FacultyName { get; set; }
        [Required(ErrorMessage = "Please enter a title.")]
        [DisplayName("Job Title")]
		public string JobTitle { get; set; }
		[Required(ErrorMessage = "Please specify where the job is being offered.")]
		[DisplayName("Location")]
		public string JobLocation { get; set; }
        [Required(ErrorMessage = "Please provide a description of what the job being offered is")]
        [DisplayName("Job Description")]
		public string JobDescription { get; set; }
		[Required(ErrorMessage = "Please provide responsibilities.")]
		[DisplayName("Key responsibilities")]
		public string Responsibilities { get; set; }
		[Required(ErrorMessage = "Select a job type.")]
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
		[Range(1, int.MaxValue, ErrorMessage = "Please enter an hourly rate.")]
		[DisplayName("Hourly rate")]
		public decimal HourlyRate { get; set; }

		[DisplayName("Limited to")]
		public bool LimitedToSA { get; set; }
		[Required(ErrorMessage = "Please provide a minimum requirements for the post.")]
		[DisplayName("Minimum requirments")]
		public string MinimumRequirement { get; set; }
		[Required(ErrorMessage = "Please provide a instructions for what should be submitted when applying.")]
		[DisplayName("Application instruction")]
		public string ApplicationInstruction { get; set; }
		[Required(ErrorMessage = "Select a closing date for the post.")]
		[DisplayName("Application closing date")]
		[DataType(DataType.Date)]
		public DateTime ApplicationClosingDate { get; set; }
		[Required(ErrorMessage = "Please provide contact name")]
		[DisplayName("Contact person name (for internal use)")]
		public string ContactPersonName { get; set; }
		[Required(ErrorMessage = "Please provide an email address for the listed contact")]
		[DisplayName("Contact person email")]
		[EmailAddress]
		public string ContactPersonEmail { get; set; }
		[Required(ErrorMessage = "Please provide a phone number for the listed contact.")]
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
