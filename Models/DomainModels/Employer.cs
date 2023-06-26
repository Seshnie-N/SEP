using SEP.Data;
using SEP.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    [Table("Employer")]
    public class Employer
    {
        [Key,ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage ="Enter your company registration number.")]
        [DisplayName("Company Registration Number")]
        
        public string CompanyRegistrationNumber { get; set; }
        [Required(ErrorMessage = "Enter the name your business is trading under.")]
        [DisplayName("Trading Name")]
        public string TradingName { get; set; }
        [Required(ErrorMessage = "Enter the name of your business.")]
        [DisplayName("Registered Business Name")]
        public string BusinessName { get; set; }
        public string Address { get; set; }
        [DisplayName("Business Type")]
        public BusinessType BusinessType { get; set; }
        [DisplayName("Account Approved")]
        public bool IsApproved { get; set; }
        [DisplayName("Approver's Note")]
        public string? ApproverNote { get; set; }
        [DisplayName("Employer Profile Approval Status")]
        public string? ApprovalStatus { get; set; }
        [DisplayName("Internal Employer?")]
        public bool IsInternal { get; set; }

	}
}
